// src/components/pages/SettingsPage.js
import React, { useState } from "react";
import axios from "axios";
import QuizSelection from "./QuizSelection";
import { useNavigate } from "react-router-dom";

const SettingPage = () => {
  const handleSetBinaryChoice = async () => {
    showBinaryChoiceQuizPage();
    try {
      const querydata = { initialId: 0 };
      const url = "binary-choice-question";
      const response = await fetch(url);
      if (!response.ok) {
        throw new Error("Failed to fetch binary choice question");
      }
      const data = await response.json();
    //   applyBinaryChoiceQuestion(data);
    } catch (error) {
      console.error("Error fetching binary choice question:", error);
      // Handle error (e.g., show error message)
    }
  };

  const handleSetMultipleChoice = async () => {
    showMultipleChoiceQuizPage();
    try {
      const querydata = { initialId: 0 };
      const url = "multiple-choice-question";
      const response = await fetch(url);
      if (!response.ok) {
        throw new Error("Failed to fetch multiple choice question");
      }
      const data = await response.json();
    //   applyMultipleChoiceQuestion(data);
    } catch (error) {
      console.error("Error fetching multiple choice question:", error);
    }
  };

  const showBinaryChoiceQuizPage = () => {
    document.getElementById("binary-choice-question-quiz-page").style.display =
      "block";
    document.getElementById(
      "multiple-choice-question-quiz-page"
    ).style.display = "none";
  };

  const showMultipleChoiceQuizPage = () => {
    document.getElementById("binary-choice-question-quiz-page").style.display =
      "none";
    document.getElementById(
      "multiple-choice-question-quiz-page"
    ).style.display = "block";
  };

  return (
    <div>
      <button onClick={handleSetBinaryChoice}>Set Binary Choice</button>
      <button onClick={handleSetMultipleChoice}>Set Multiple Choice</button>
    </div>
  );
};

export default SettingPage;
