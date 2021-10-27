import { useEffect } from 'react';
import { userManager } from '../components/OidcClientProvider';

// ? ------------------------------- Components ----------------------------------
export default function SignInCallback() {
  useEffect(() => {
    userManager.signinPopupCallback();
  }, []);

  return <span>Authenticating</span>;
}
