import { combineReducers } from 'redux';
import users from './login.reducer';

const rootReducer = combineReducers({
  users
});

export default rootReducer;