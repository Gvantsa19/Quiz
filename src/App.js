import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import MainPage from "./components/pages/MainPage";
import Register from "./components/auth/Register";
import Home from "./components/Home";
import "bootstrap/dist/css/bootstrap.min.css";
// import AppRoutes from './AppRoutes';
// import { PublicLayout } from './components/Layout/main';
// import { AuthProvider } from './components/auth/AuthProvider';
// import { Container } from 'reactstrap';
// import AppRoutesProtected from './AppRoutesProtected';
// import { ProtectedRoute } from './components//auth/ProtectedRoute';
// import { PrivateLayout } from './components/Layout/PrivateLayout';
// import Login from "./components/auth/Login";
// import SettingPage from "./components/pages/SettingPage";
import "primereact/resources/themes/vela-blue/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";
// import { PrimeReactProvider, PrimeReactContext } from "primereact/api";
// import 'primeflex/primeflex.css';

function App() {
  return (
    <Router>
      <Routes>
        {" "}
        {/* Use Routes container */}
        <Route exact path="/" element={<Home />} />
        <Route path="/register" element={<Register />} />
        <Route path="/mainPage" element={<MainPage />} />
      </Routes>{" "}
      {/* Close Routes */}
    </Router>
  );
}

export default App;

// export const App = () => {
//   return (
//     <>
//       <AuthProvider>
//         <Routes>
//           {AppRoutes.map((route, index) => {
//             const { element, ...rest } = route;
//             return (
//               <Route
//                 key={index}
//                 {...rest}
//                 element={
//                   <PublicLayout>
//                     <Container>{element}</Container>
//                   </PublicLayout>
//                 }
//               />
//             );
//           })}
//           {AppRoutesProtected.map((route, index) => {
//             const { element, ...rest } = route;
//             return (
//               <Route
//                 key={index}
//                 {...rest}
//                 element={
//                   <PrivateLayout>
//                     <ProtectedRoute>
//                       <Container>{element}</Container>
//                     </ProtectedRoute>
//                   </PrivateLayout>
//                 }
//               />
//             );
//           })}
//         </Routes>
//       </AuthProvider>
//     </>
//   );
// };
