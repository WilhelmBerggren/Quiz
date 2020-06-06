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

    return (
        <>
            <h3>Highscore:</h3>
            {!data.loading && data.scores.map(s => (
                <>
                    <h3>{s.user}: {s.score}</h3>
                </>
            ))}
        </>
    );
}



export const Game = () => {
    const [data, setData] = useState({questions: [], loading: true});
    const [progress, setProgress] = useState(null);
    const [score, setScore] = useState(0);
    const [gameOver, setGameOver] = useState(false);

    const PostScore = async (score) => {
        const token = await authService.getAccessToken();
        const user = await authService.getUser();
        console.log(user);
        await fetch('api/ScoreItems',
        {
            headers: !token ? {} : { 
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json' },
            method: "POST",
            body: JSON.stringify({
                "user": user.name,
                "score": score
            }),
        })
        console.log(user.Email);
        setGameOver(true);
    }

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
            setData({ questions: data.sort((a,b) => 0,5 - Math.random()), loading: false });
        }
        fetchData();
    }, []);

    if(!gameOver && data.questions.length < progress){
        PostScore(score)
    }
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
    const options = [answer.answer, answer.option1, answer.option2].sort((a, b) => 0.5 - Math.random());
    const renderOption = (correct, answer) => <button onClick={() => answer(correct)}>{answer}</button>
    return (
        <div id="question">
            <h3>{question.title}</h3>
            {options.map(o => renderOption(o == question.answer, o))}
        </div>
    );
}
