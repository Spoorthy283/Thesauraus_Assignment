import React from 'react';
import Main from './components/MainComponent';
import ConfigureStore from "./redux/ConfigureStore";
import './custom.css'
import { Provider } from 'react-redux';

const store = ConfigureStore();
export default () => (
    <Provider store={store}>
        <Main />
    </Provider>
);