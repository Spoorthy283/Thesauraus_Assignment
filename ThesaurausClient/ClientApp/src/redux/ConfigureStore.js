import { createStore, combineReducers, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import logger from 'redux-logger';
import { WordStore } from './WordStore';
import { SynonymsStore } from './SynonymsStore';
import { AddWordStore } from './AddWordStore';

 const ConfigureStore = () => {
    const store = createStore(combineReducers({
        wordstore: WordStore,
        synonymsstore: SynonymsStore,
        addwordstore: AddWordStore
    }), applyMiddleware(thunk, logger));

    return store;
}
export default ConfigureStore;