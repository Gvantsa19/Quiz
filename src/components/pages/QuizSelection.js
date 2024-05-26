import React, { useState } from 'react';
import BinaryChoiceQuestion from "../quiz/BinaryChoiceQuestion";
import multipleChoiceQuiz from "../quiz/multipleChoiceQuiz";

const QuizSelection = ({ onQuizSelect }) => {
  const [showBinaryQuiz, setShowBinaryQuiz] = useState(true);

  const handleModeChange = () => {
    setShowBinaryQuiz(prevState => !prevState);
  };

  return (
    <div>
      <h1>Famous Quote Quiz</h1>
      {showBinaryQuiz ? (
        <BinaryChoiceQuestion />
      ) : (
        <multipleChoiceQuiz />
      )}
      <button onClick={handleModeChange}>
        Switch Mode
      </button>
    </div>
  );
};

export default QuizSelection;
