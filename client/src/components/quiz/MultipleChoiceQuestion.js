import React, { useState, useEffect } from "react";
import axios from "axios";
import Button from "react-bootstrap/Button";
import Config from "../../config.json";

const MultipleChoiceQuestionPage = ({ baseServiceQuizUrl }) => {
  const [questionData, setQuestionData] = useState({
    id: "",
    correctAuthorId: "",
    quote: "",
    authors: [],
  });

  const [questionsData, setQuestionsData] = useState({
    id: "",
    correctAuthorId: "",
    quote: "",
    authors: [],
  });
  const [selectedAuthorId, setSelectedAuthorId] = useState(null);
  const [showResult, setShowResult] = useState(false);

  const [currentIndex, setCurrentIndex] = useState(0);

  const getAuthHeaders = () => {
    const token = localStorage.getItem("token");
    return { Authorization: `Bearer ${token}` };
  };

  useEffect(() => {
    fetchQuestionData();
  }, []);

  useEffect(() => {
    const fetchLastMultipleChoiceQuestionId = async () => {
      try {
        const response = await fetch(
          Config.Settings.CommonApi_BaseUrl + "Quiz/listOfMultiple",
          {
            headers: getAuthHeaders(),
          }
        );
        if (!response.ok) {
          throw new Error("Failed to fetch last multiple choice question");
        }
        const data = await response.json();
        setQuestionsData(data);
        storeLastMultipleChoiceQuestionId(data);
      } catch (error) {
        console.error("Error fetching last multiple choice question:", error);
      }
    };

    fetchLastMultipleChoiceQuestionId();
  }, [currentIndex]);

  const storeLastMultipleChoiceQuestionId = (response) => {
    localStorage.setItem("lastMultipleChoiceQuestionId", response.id);
  };

  const fetchQuestionData = async () => {
    const response = await axios.get(
      Config.Settings.CommonApi_BaseUrl + "Quiz/multiple-choice-question",
      {
        headers: getAuthHeaders(),
      }
    );
    const data = response.data;
    setQuestionData(data);
  };

  const handleAnswerClick = (authorId) => {
    setSelectedAuthorId(authorId);
    setShowResult(true);
  };

  const handleNextClick = () => {
    setShowResult(false);
    setSelectedAuthorId(null);
    setCurrentIndex((prevIndex) => prevIndex + 1);
  };

  const renderAnswerOptions = () => {
    const currentQuestion = questionsData[currentIndex];
    const shuffledAuthors = currentQuestion.authors
      .filter((author) => author.id !== currentQuestion.correctAuthorId)
      .sort(() => Math.random() - 0.5)
      .slice(0, 3);

    const options = shuffledAuthors.map((author) => (
      <Button
        style={{
          marginRight: "15px",
          padding: "20px",
          fontSize: "20px",
          fontWeight: "bold",
        }}
        variant="primary"
        key={author.id}
        onClick={() => handleAnswerClick(author.id)}
      >
        {author.name}
      </Button>
    ));

    options.push(
      <Button
        style={{
          marginRight: "15px",
          padding: "20px",
          fontSize: "20px",
          fontWeight: "bold",
        }}
        variant="primary"
        key={currentQuestion.correctAuthorId}
        onClick={() => handleAnswerClick(currentQuestion.correctAuthorId)}
      >
        {
          currentQuestion.authors.find(
            (author) => author.id === currentQuestion.correctAuthorId
          ).name
        }
      </Button>
    );

    return options;
  };

  return (
    <div
      style={{
        width: "1000px",
        height: "900px",
        boxShadow: "rgb(38, 57, 77) 0px 20px 30px -10px",
        padding: "50px",
        backgroundImage: "linear-gradient(120deg, #e0c3fc 0%, #8ec5fc 100%)",
      }}
    >
      <h1>Who Said It? Famous Quotes Quiz</h1>
      {questionsData.length > 0 && currentIndex < questionsData.length ? (
        <div style={{ marginTop: "100px" }}>
          <p
            style={{
              fontSize: "32px",
              fontWeight: "bold",
              textAlign: "center",
            }}
          >
            "{questionsData[currentIndex].quote}"
          </p>
          {!showResult && (
            <div
              style={{
                display: "flex",
                justifyContent: "center",
                marginTop: "100px",
              }}
            >
              {renderAnswerOptions()}
            </div>
          )}
          {showResult && (
            <div>
              {selectedAuthorId ===
              questionsData[currentIndex].correctAuthorId ? (
                <p
                  style={{
                    fontSize: "22px",
                    marginTop: "70px",
                    color: "green",
                    textAlign: "center",
                    fontWeight: "bold",
                  }}
                >
                  Correct! The right answer is:{" "}
                  {questionsData[currentIndex].correctAuthorId}
                </p>
              ) : (
                <p
                  style={{
                    fontSize: "22px",
                    marginTop: "70px",
                    color: "red",
                    textAlign: "center",
                    fontWeight: "bold",
                  }}
                >
                  Sorry, you are wrong! The right answer is:{" "}
                  {questionsData[currentIndex].correctAuthorId}
                </p>
              )}
              <Button
                style={{
                  marginLeft: "40%",
                  marginTop: "50px",
                  padding: "20px",
                  color: "white",
                  width: "200px",
                }}
                variant="info"
                onClick={handleNextClick}
              >
                Next
              </Button>
            </div>
          )}
        </div>
      ) : (
        <div style={{ textAlign: "center", marginTop: "100px" }}>
          <h2>Quiz is over! Thank you for participating.</h2>
        </div>
      )}
    </div>
  );
};

export default MultipleChoiceQuestionPage;
