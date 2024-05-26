// src/AppRoutesProtected.js
import MainPage from "./components/pages/MainPage";
import SettingsPage from "./components/pages/SettingPage";

const AppRoutesProtected = [
    {
        path: '/main',
        element: <MainPage />
    },
    // {
    //     path: '/submenu/:ItemID',
    //     element: <MainPage />
    // },
    {
        path: '/settings',
        element: <SettingsPage />
    },
];

export default AppRoutesProtected;
