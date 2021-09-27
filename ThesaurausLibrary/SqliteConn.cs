using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ThesaurausLibrary
{
    class SqliteConnectionn
    {
        
        string connectionStr = "Data Source=.\\TestLib21.db3";
        /// <summary>
        /// add word and synonyms
        /// </summary>
        /// <param name="wordStr">word</param>
        /// <param name="synonyms">synonyms</param>
        /// <returns></returns>
        internal bool AddWord(string wordStr, List<string> synonyms)
        {

            try
            {
                string path = Directory.GetCurrentDirectory();
                string word = wordStr.ToLower();
                using (var connection = new SqliteConnection(connectionStr))
                {
                    connection.Open();


                    using (var transaction = connection.BeginTransaction())
                    {
                        var insertCmd = connection.CreateCommand();
                        insertCmd.CommandText = $"INSERT OR IGNORE INTO Words (Word) VALUES ('{word}'); select Id from Words where Word='{word}' ";

                        int wordId = Convert.ToInt32(insertCmd.ExecuteScalar());

                        foreach (string synonymStr in synonyms)
                        {
                            string synonym = synonymStr.ToLower();
                            insertCmd.CommandText = $"INSERT OR IGNORE INTO Words (Word) VALUES ('{synonym}'); select Id from Words where Word='{synonym}' ";
                            int synonymId = Convert.ToInt32(insertCmd.ExecuteScalar());

                            insertCmd.CommandText = $"INSERT OR IGNORE INTO WordSynonyms (WordId,SynonymId) values({wordId},{synonymId})";
                            insertCmd.ExecuteNonQuery();

                        }
                        transaction.Commit();
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                saveExceptionToDB($"Error occured while adding word and synonyms: {wordStr}", e);
                return false;
            }
            return true;
        }

        /// <summary>
        /// get synonyms for word
        /// </summary>
        /// <param name="word">word</param>
        /// <returns></returns>
        internal List<string> getSynonyms(string word)
        {
            string Path = Environment.CurrentDirectory;
            List<string> synonyms = new List<string>();
            if (word != null)
            {
                try
                {

                    using (var connection = new SqliteConnection(connectionStr))
                    {
                        connection.Open();

                        var selectCmd = connection.CreateCommand();
                        selectCmd.CommandText = @$"
                            select w2.Word from WordSynonyms ws
                            inner join Words w1 on ws.WordId = w1.Id
                            inner join Words w2 on ws.SynonymId = w2.Id
                            where w1.Word='{word.ToLower()}'
                       ";

                        using (var reader = selectCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                synonyms.Add(reader.GetString(0));
                            }
                        }
                        connection.Close();
                    }

                }
                catch (Exception e)
                {
                    saveExceptionToDB($"Error occured while fetching synonyms for word: {word}", e);
                }
            }
            return synonyms;
        }

        /// <summary>
        /// get all words and synoyms
        /// </summary>
        /// <returns></returns>
        internal Dictionary<string, List<string>> getAllWords()
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            try
            {
                using (var connection = new SqliteConnection(connectionStr))
                {
                    connection.Open();

                    var selectCmd = connection.CreateCommand();
                    selectCmd.CommandText = @"
                            select w1.Word as Word, w2.Word as Synonym from WordSynonyms ws
                            inner join Words w1 on ws.WordId = w1.Id
                            inner join Words w2 on ws.SynonymId = w2.Id
                        ";

                    using (var reader = selectCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string word = reader.GetString(0).ToLower();
                            string synonym = reader.GetString(1).ToLower();

                            if (dict.ContainsKey(word))
                            {
                                List<string> synonymsList;
                                dict.TryGetValue(word, out synonymsList);
                                if (synonymsList != null)
                                    synonymsList.Add(synonym);
                                dict[word] = synonymsList;
                            }
                            else
                            {
                                List<string> newSynonymsList = new List<string>();
                                newSynonymsList.Add(synonym);
                                dict.Add(word.ToLower(), newSynonymsList);
                            }
                        }
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                saveExceptionToDB("Error occured while fetching all words", e);
            }
            return dict;
        }

        /// <summary>
        /// Save exception and message to db
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        private void saveExceptionToDB(string message, Exception e)
        {
            StringBuilder exceptionStr = new StringBuilder();
            exceptionStr.Append($"Ex msg: {e.Message};");
            if (e.InnerException != null)
            {
                exceptionStr.Append($" InnerException:" + e.InnerException.Message);
            }

            using (var connection = new SqliteConnection(connectionStr))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = $"INSERT INTO ErrorLog (Message,Exception) VALUES ('{message}','{exceptionStr}')";
                    insertCmd.ExecuteNonQuery();
                }
                connection.Close();
            }

        }

    }
}
