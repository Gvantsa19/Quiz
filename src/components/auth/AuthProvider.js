import React, { useState, useEffect } from 'react';
import { useLocation } from "react-router-dom";
import Config from "../../config.json";
import { getCurrentUserFromJwtToken } from "../../js/Utils.js"


export const AuthContext = React.createContext(null);
export const useAuth = () => { return React.useContext(AuthContext); };

export const AuthProvider = ({ children }) => {
    const location = useLocation();
    const [setDefaultModule] = useState('');

    useEffect(() => {
        const jCurrentUser = localStorage.getItem("CurrentUser");
        let isCrossLogin = manageCrossLogin();

        if (isCrossLogin)
            return;

        if (jCurrentUser !== null && jCurrentUser !== undefined) {
            let currentUser = JSON.parse(jCurrentUser);

            let today = new Date();
            if (!currentUser.expire || new Date(currentUser.expire).getTime() < today.getTime()) {
                handleLogout();
            }

        } else {
            const onCallLoginAsAnonymous = async () => {
                const response = await loginAnonymous();

                if (response.success === true && response.isanonymous) {
                    handleLogin(response.token, null, null, response.isanonymous, null, response.startModule);
                    window.location.href = Config.Settings.Base_Url;
                }
            }

            onCallLoginAsAnonymous();
        }

    }, []);
    function manageCrossLogin() {
        const loginWithParams = async (credentials, redirectUrl) => {
            const requestOption = {
                method: 'POST',
                credentials: 'include',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(credentials)
            };

            let path = credentials.ssoToken !== null && credentials.ssoToken !== undefined
                ? Config.Settings.CommonApi_BaseUrl + 'User/login'
                : Config.Settings.CommonApi_BaseUrl + 'User/login';

            try {
                const fetchedRes = await fetch(path, requestOption);
                const response = await fetchedRes.json();

                if (response.success === true) {
                    handleLogin(response.token, redirectUrl, response.impersonate, response.isanonymous, response.addToOrder, response.startModule);
                }

            } catch (ex) {
                console.error(ex);
            }
        }

        let params = new URLSearchParams(window.location.search);
        let mySSOToken = params.get('ssoToken');
        let mySTkn = params.get('sTkn');
        let redirectUrl = params.get('ru');
        let autoLoginCode = params.get('ual');
        let crossLoginVersion = params.get('xl');

        if (autoLoginCode) {
            autoLogin(autoLoginCode, redirectUrl);
            return true;
        }

        if (!mySSOToken && !mySTkn)
            return false;
        else {
            loginWithParams({
                username: null,
                password: null,
                ssoToken: mySSOToken,
                sessionToken: mySTkn,
                crossLoginVersion: crossLoginVersion
            }, redirectUrl).catch(console.error);
        }

        return true;
    }
    async function autoLogin(authCode, redirectUrl) {
        let orderInfo = { authCode: authCode, websiteId: Config.Settings.WebsiteId }
        const requestOption = { method: 'POST', credentials: 'include', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(orderInfo) };
        const response = await fetch(Config.Settings.CommonApi_BaseUrl + 'auth/AutoLogin', requestOption).then(data => data.json());

        if (response.success) {
            if (redirectUrl !== null && redirectUrl !== undefined) {
                window.location.href = redirectUrl;
            }
        }
    }
    async function logoutUser() {
        return fetch(Config.Settings.CommonApi_BaseUrl + 'auth/logout/' + Config.Settings.WebsiteId, { credentials: 'include' }).then(data => data.json());
    }
    async function loginAnonymous() {
        let credentials = {
            websiteId: Config.Settings.WebsiteId
        };

        const requestOption = {
            method: 'POST',
            credentials: 'include',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(credentials)
        };

        return fetch(Config.Settings.CommonApi_BaseUrl + 'auth/loginanonymous', requestOption).then(data => data.json());
    }

    const handleLogin = (newToken, redirectUrl, impersonate, isanonymous) => {
        debugger;
        let currentUser = getCurrentUserFromJwtToken(newToken);

        if (currentUser.name === null || currentUser.name === undefined || currentUser.name === "")
            currentUser.name = currentUser.username;

        if (impersonate !== null)
            currentUser.impersonate = impersonate;

        if (isanonymous !== null)
            currentUser.isanonymous = isanonymous;

        let jCurrentUser = JSON.stringify(currentUser);
        localStorage.setItem("CurrentUser", jCurrentUser);

        let origin = redirectUrl !== null && redirectUrl !== undefined ? redirectUrl : location.state?.from?.pathname || Config.Settings.Base_Url + "home";
        if (!origin)
            origin = 'engine';

        window.location.href = origin;
    };

    const handleLogout = () => {
        const onCallLogout = async () => {
            const response = await logoutUser();

            if (response.success === true) {
                localStorage.removeItem("CurrentUser");
                localStorage.removeItem("QuotationInfo");
                localStorage.removeItem("WelfareInfo");
                localStorage.removeItem("AddToOrder");

                if (response.isanonymous)
                    handleLogin(response.token, null, null, response.isanonymous, null, response.startModule);

                window.location.href = Config.Settings.Base_Url;
            }
        }

        onCallLogout();
    };

    const value = {
        onLogin: handleLogin,
        onLogout: handleLogout
    };

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
};