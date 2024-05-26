import React from 'react';

export const Loading = ({ textMsg, isSmall }) => {
    return (
        <>
            {
                !isSmall && <div className="loading text-center"><img src="https://cdn.travelplace.ch/common/images/general/waiting-page.gif" alt="" /><h6>{textMsg}</h6></div>
            }
            {
                isSmall && <div className="text-center"><div className="spinner-border spinner-border-sm text-dark" role="status"><span className="sr-only">{textMsg}</span></div></div>
            }
        </>
    );
}
