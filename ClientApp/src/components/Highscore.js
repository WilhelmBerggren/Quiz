import React, { useState, useEffect } from 'react';
import authService from './api-authorization/AuthorizeService'

export const Highscore = () => {
    const [data, setData] = useState({scores: [], loading: true});
    
    useEffect(() => {
        async function fetchData() {
            const token = await authService.getAccessToken();
            const response = await fetch('api/ScoreItems', {
                headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
              });
            const data = await response.json();
            setData({ scores: data, loading: false });
        }
        fetchData();
    }, []);

    const sortHighscore = (a, b) => {
        if(a.score > b.score) return -1;
        if(a.score < b.score) return 1;
        if(a.date > b.score) return -1;
        if(a.date <= b.score) return 1;
    }

    return (
        <>
            <h3>Highscore:</h3>
            {!data.loading && data.scores.sort(sortHighscore).map(s => (
                <p key={s.id}>{s.user}: {s.score}</p>
            ))}
        </>
    );
}
