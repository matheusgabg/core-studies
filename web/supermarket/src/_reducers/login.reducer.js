import { usersConstants } from '../_constants/users.constants';

const initialState = {
    currentUser: {}
  }
  
  export default function users(state = initialState, action) {
      switch (action.type) {
        case usersConstants.LOGIN_USERS:
          return {...state, currentUser: action.payload}
        default:
          return state;
      }
    }