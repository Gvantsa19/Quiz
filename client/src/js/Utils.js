
const getAuthHeaders = () => {
    const token = localStorage.getItem("token");
    return {
      Authorization: `Bearer ${token}`,
    };
  };
export const decodeJwtToken = (jwtToken) => {
    let base64Url = jwtToken.split('.')[1];
    let base64 = base64Url.replace('-', '+').replace('_', '/');
    return JSON.parse(window.atob(base64));
}

export const getCurrentUserFromJwtToken = (jwtToken) => {
    let oJwtDecoded = decodeJwtToken(jwtToken);

    let currentUser = {
        username: oJwtDecoded["Username"],
        userId: oJwtDecoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
        name: oJwtDecoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"].trim(),
        token: jwtToken,
        expire: new Date(oJwtDecoded.exp * 1000),
    };

    return currentUser;
}
