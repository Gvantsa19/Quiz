import React from 'react';
import { SignIn } from './auth/SignIn';
import { getAuthToken } from '../js/Utils.js';
import configData from "../config.json";
import { useTranslation } from 'react-i18next';


export const Hometwo = () => {
    const { t } = useTranslation();

    let token = getAuthToken();

    let params = new URLSearchParams(window.location.search);
    let mySSOToken = params.get('ssoToken');
    let mySTkn = params.get('sTkn');
    let addToOrder = params.get('ato');
    let userAutoLogin = params.get('ual');

    if (token) {
        let origin = configData.Settings.Base_Url + "home";
        if (!origin)
            origin = 'engine';
        window.location.href = origin;
    } else {
        return (
            <>
                <div className="mt-6">
                    <h3 className="text-center">{t(`home:welcome`)}</h3>
                    {!token || (mySSOToken !== null || mySTkn !== null || addToOrder !== null || userAutoLogin !== null) ? (<SignIn />) : (<></>)}
                </div>
            </>
        );
    }
}
