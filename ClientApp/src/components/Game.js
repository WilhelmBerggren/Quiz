import React, { useState, useEffect } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Highscore } from './Highscore';

export const Game = () => {
    const [data, setData] = useState({questions: [], loading: true});
    const [progress, setProgress] = useState(null);
    const [score, setScore] = useState(0);
    const [gameOver, setGameOver] = useState(false);

    const restart = () => {
        setScore(0);
        setProgress(null);
        setGameOver(false);
    }

    const postScore = async (score) => {
        if(score == 0) return;
        const token = await authService.getAccessToken();
        const user = await authService.getUser();
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
            const sorted = data.sort((a,b) => 0.5 - Math.random());
            console.log(data);
            console.log(sorted);
            setData({ questions: sorted, loading: false });
        }
        fetchData();
    }, []);

    if(!gameOver && data.questions.length < progress){
        postScore(score)
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
            {!data.loading && (data.questions.length < progress) && (
                <>
                    <h3>
                        Thanks for playing!
                    </h3>
                    <Highscore />
                    <p>Score: {score}</p>
                    <button onClick={() => restart()}>Restart</button>
                </>
            )}

        </>
    );
}

const Question = ({question, answer}) => {
    const options = [question.answer, question.option1, question.option2].sort((a, b) => 0.5 - Math.random());
    const renderOption = (correct, title) => <button key={title} onClick={() => answer(correct)}>{title}</button>
    return (
        <div id="question">
            <h3>{question.title}</h3>
            {options.map(o => renderOption(o == question.answer, o))}
        </div>
    );
}
