import { useReducer, useEffect, createContext, useContext } from 'react';
import { UserManager } from 'oidc-client';
import axios from '../utils/axios';
// ? -------------------------------- Constants ----------------------------------

export const userManager = new UserManager({
  authority: 'https://localhost:5001/.well-known/openid-configuration',
  client_id: 'tracking-client',
  scope: 'openid profile offline_access',
  popup_redirect_uri: 'http://localhost:3111/auth/sign-in-callback',
  response_type: 'code',
  monitorSession: false
});

// ? --------------------------------- Reducer -----------------------------------

const reducer = {
  initialed: (state, { payload }) => ({
    ...state,
    isInitialed: true,
    user: (payload && payload.user) || null,
    isAuthentication: Boolean(payload && payload.user)
  })
};

const handlerDispatch = (state, action) => (reducer[action.type] ? reducer[action.type](state, action) : state);

const initState = {
  isInitialed: false,
  isAuthentication: false,
  user: null
};

const AuthContext = createContext({
  initState
});

export const useAuth = () => useContext(AuthContext);

// ? -------------------------------- Component ----------------------------------

export default function OidcClientProvider({ children }) {
  const [state, dispatch] = useReducer(handlerDispatch, initState);

  useEffect(() => {
    userManager
      .signinPopup()
      .then((user) => {
        axios.defaults.headers = {
          Authorization: `Bearer ${user.access_token}`
        };
        dispatch({
          type: 'initialed',
          payload: user
        });
      })
      .catch(() => {
        dispatch({
          type: 'initialed',
          payload: null
        });
      });
  }, []);

  console.log(state.isInitialed);
  if (!state.isInitialed) {
    return <span>Loading...</span>;
  }

  return <AuthContext.Provider value={state}>{children}</AuthContext.Provider>;
}
