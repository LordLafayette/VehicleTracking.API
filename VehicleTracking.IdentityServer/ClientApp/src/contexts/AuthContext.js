import { createContext, useReducer } from 'react';
import PropTypes from 'prop-types';
// utils
import axios from '../utils/axios';

// ----------------------------------------------------------------------

const initialState = {
  isAuthenticated: false,
  true: false,
  user: null
};

const handlers = {
  INITIALIZE: (state, action) => {
    const { isAuthenticated, user } = action.payload;
    return {
      ...state,
      isAuthenticated,
      isInitialized: true,
      user
    };
  },
  LOGIN: (state, action) => {
    const { user } = action.payload;

    return {
      ...state,
      isAuthenticated: true,
      user
    };
  },
  LOGOUT: (state) => ({
    ...state,
    isAuthenticated: false,
    user: null
  }),
  REGISTER: (state, action) => {
    const { user } = action.payload;

    return {
      ...state,
      isAuthenticated: true,
      user
    };
  }
};

const reducer = (state, action) => (handlers[action.type] ? handlers[action.type](state, action) : state);

const AuthContext = createContext({
  ...initialState,
  method: 'jwt',
  login: () => Promise.resolve(),
  logout: () => Promise.resolve(),
  register: () => Promise.resolve()
});

AuthProvider.propTypes = {
  children: PropTypes.node
};

function AuthProvider({ children }) {
  const [state, dispatch] = useReducer(reducer, initialState);

  const login = async (username, password) => {
    const form = new FormData();
    form.append('username', username);
    form.append('password', password);
    const response = await axios.post('/account/login', form);
    if (response.data && !response.data.succeeded) {
      throw Error('username or password incorrect.');
    }
    // dispatch({
    //   type: 'LOGIN',
    //   payload: {
    //     user
    //   }
    // });
  };

  const register = async (data) => {
    const response = await axios.post('/api/account/register', data);

    if (response.data && response.data.errors && response.data.errors.length) {
      throw Error(response.data.errors[0].description);
    }
  };

  const logout = async () => {
    dispatch({ type: 'LOGOUT' });
  };

  return (
    <AuthContext.Provider
      value={{
        ...state,
        method: 'jwt',
        login,
        logout,
        register
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export { AuthContext, AuthProvider };
