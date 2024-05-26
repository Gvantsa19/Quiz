import React, { useState } from "react";
import BinaryChoiceQuestion from "../quiz/BinaryChoiceQuestion";
import MultipleChoiceQuestionPage from "../quiz/MultipleChoiceQuestion";
import Tab from "react-bootstrap/Tab";
import Tabs from "react-bootstrap/Tabs";
import Button from "react-bootstrap/Button";

const MainPage = () => {
  const [questionsData] = useState([
    {
      type: "binary",
    },
    {
      type: "multiple",
    },
  ]);
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
  const [quizStarted, setQuizStarted] = useState(false);
  const [selectedQuestionType, setSelectedQuestionType] = useState(null);

  const handleNextQuestion = () => {
    setCurrentQuestionIndex(
      (prevIndex) => (prevIndex + 1) % questionsData.length
    );
  };

  const renderCurrentQuestion = () => {
    const question = questionsData[currentQuestionIndex];

    if (selectedQuestionType === "binary" || !selectedQuestionType) {
      return (
        <BinaryChoiceQuestion
          key={currentQuestionIndex}
          questionData={question}
        />
      );
    } else if (selectedQuestionType === "multiple") {
      return (
        <MultipleChoiceQuestionPage
          key={currentQuestionIndex}
          questionData={question}
        />
      );
    } else {
      return (
        <MultipleChoiceQuestionPage
          key={currentQuestionIndex}
          questionData={question}
        />
      );
    }
  };

  const handleStartQuiz = () => {
    setQuizStarted(true);
  };

  return (
    <div
      style={{
        width: "100%",
        height: "100%",
      }}
    >
      <h1>please select quiz tab to start game</h1>
      <Tabs
        style={{
          marginTop: "60px",
          fontSize: "30px",
          width: "250px",
          marginLeft: "40%",
        }}
        defaultActiveKey="profile"
        id="uncontrolled-tab-example"
      >
        <Tab eventKey="Quiz" title="Quiz">
          <div>
            {!quizStarted && (
              <Button
                variant="success"
                style={{
                  marginTop: "300px",
                  padding: "25px",
                  fontSize: "30px",
                  marginLeft: "50%",
                }}
                onClick={handleStartQuiz}
              >
                Start Quiz
              </Button>
            )}
            {quizStarted && (
              <div
                className="main-container"
                style={{
                  display: "flex",
                  justifyContent: "center",
                  alignItems: "center",
                  height: "100vh",
                  width: "100%",
                }}
              >
                {renderCurrentQuestion()}
                {/* <button onClick={handleNextQuestion}>Next Question</button> */}
              </div>
            )}
          </div>
        </Tab>
        <Tab eventKey="Settings" title="Settings">
          <div
            style={{
              width: "1000px",
              height: "500px",
              boxShadow: "rgb(38, 57, 77) 0px 20px 30px -10px",
              padding: "50px",
              marginTop: "200px",
              marginLeft:"30%",
              backgroundImage: "linear-gradient(120deg, #e0c3fc 0%, #8ec5fc 100%)"
            }}
          >
            <h1>Select question type:</h1>

            <div
              style={{
                display: "flex",
                justifyContent: "center",
                width: "100%",
                height: "100%",
                marginTop: "100px",
              }}
            >
              <Button
                style={{
                  color: "white",
                  width: "150px",
                  height: "50px",
                  marginRight: "50px",
                  fontSize: "22px",
                  fontWeight: "bold",
                }}
                variant={
                  selectedQuestionType === "binary" ? "success" : "primary"
                }
                onClick={() => setSelectedQuestionType("binary")}
              >
                Binary
              </Button>
              <Button
                style={{
                  color: "white",
                  width: "150px",
                  height: "50px",
                  fontSize: "22px",
                  fontWeight: "bold",
                }}
                variant={
                  selectedQuestionType === "multiple" ? "success" : "primary"
                }
                onClick={() => setSelectedQuestionType("multiple")}
              >
                Multiple
              </Button>
            </div>
          </div>
        </Tab>
      </Tabs>
    </div>
  );
};

export default MainPage;
