import React, { useState, useEffect } from 'react';
import authService from './api-authorization/AuthorizeService'

export const Highscore = () => {
    const [data, setData] = useState({scores: [], loading: true});
    
    useEffect(() => {
        async function fetchData() {
            const response = await fetch('api/ScoreItems');
            const data = await response.json();
            setData({ scores: data, loading: false });
        }
        fetchData();
    }, []);

    return (
        <>
            <h3>Highscore:</h3>
            {data.scores.map(s => (
                <>
                    <h3>{s.userid}: {s.score}</h3>
                </>
            ))}
        </>
    );
}

export const Game = () => {
    const [data, setData] = useState({questions: [], loading: true});
    const [progress, setProgress] = useState(null);
    const [score, setScore] = useState(0);

    const answer = (correct) => {
        setProgress(progress + 1);
        setScore(correct ? score + 1 : score);
    }

    useEffect(() => {
        async function fetchData() {
            const token = await authService.getAccessToken();
            const response = await fetch('api/QuizItems', {
                headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
              });
            const data = await response.json();
            setData({ questions: data, loading: false });
        }
        fetchData();
    }, []);

    console.log(data.questions, progress, score);
    return (
        <>
            {!progress && (
                <>
                    <h3>Press button to start!</h3>
                    <button onClick={() => setProgress(progress + 1)}>Start</button>
                </>
            )}
            {progress && data.questions.length >= progress && (
                <>
                    <Question answer={answer} question={data.questions[progress - 1]} />
                    <h3>Question: {progress} / {data.questions.length} </h3>
                </>
            )}
            {data.questions.length < progress && (
                <>
                    <h3>
                        Thanks for playing!
                    </h3>
                    <Highscore />
                    <p>Score: {score}</p>
                </>
            )}

        </>
    );
}

const Question = ({question, answer}) => {
    return (
        <div id="question">
            <h3>{question.title}</h3>
            <button onClick={() => answer(true)}>{question.answer}</button>
            <button onClick={() => answer(false)}>{question.option1}</button>
            <button onClick={() => answer(false)}>{question.option2}</button>
        </div>
    );
}
