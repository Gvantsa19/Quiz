import React, { useEffect, useState } from "react";
import Button from "react-bootstrap/Button";

const BinaryChoiceQuestion = () => {
  const [questionsData, setQuestionsData] = useState([]);
  const [currentIndex, setCurrentIndex] = useState(0);
  const [selectedAnswer, setSelectedAnswer] = useState(null);
  const [showResult, setShowResult] = useState(false);
  const [quizOver, setQuizOver] = useState(false);
  const [randomAuthor, setRandomAuthor] = useState(null);

  const getAuthHeaders = () => {
    const token = localStorage.getItem("token");
    return { Authorization: `Bearer ${token}` };
  };

  useEffect(() => {
    fetchQuestionsData();
  }, []);

  useEffect(() => {
    if (questionsData.length > 0) {
      pickRandomAuthor();
    }
  }, [currentIndex, questionsData]);

  const fetchQuestionsData = async () => {
    try {
      const response = await fetch("https://localhost:7264/api/Quiz/list", {
        headers: getAuthHeaders(),
      });
      if (!response.ok) {
        throw new Error("Failed to fetch questions");
      }
      const data = await response.json();
      if (data.length === 0) {
        setQuizOver(true);
        return;
      }
      setQuestionsData(data);
    } catch (error) {
      console.error("Error fetching questions:", error);
    }
  };

  const pickRandomAuthor = () => {
    const authors = [
      { id: 1, name: "Stephen King" },
      { id: 2, name: "Ernest Hemingway" },
      { id: 3, name: "John Brown" },
      { id: 4, name: "Ivan Ivanov" },
      { id: 5, name: "Mark Twain" },
    ];

    const randomIndex = Math.floor(Math.random() * authors.length);
    setRandomAuthor(authors[randomIndex]);
  };

  const handleAnswerClick = (answer) => {
    setSelectedAnswer(answer);
    setShowResult(true);
  };

  const handleNextClick = () => {
    setShowResult(false);
    setSelectedAnswer(null);
    if (currentIndex < questionsData.length - 1) {
      setCurrentIndex((prevIndex) => prevIndex + 1);
    } else {
      setQuizOver(true);
    }
  };

  return (
    <div
      style={{
        width: "1000px",
        height: "900px",
        boxShadow: "rgb(38, 57, 77) 0px 20px 30px -10px",
        padding: "50px",
        backgroundImage: "linear-gradient(120deg, #e0c3fc 0%, #8ec5fc 100%)"
      }}
    >
      <h1>Who Said It? Famous Quotes Quiz</h1>
      {quizOver ? (
        <p
          style={{
            fontSize: "32px",
            fontWeight: "bold",
            textAlign: "center",
          }}
        >
          Quiz is over! Thank you for participating.
        </p>
      ) : (
        <div>
          {questionsData.length > 0 && randomAuthor && (
            <div>
              <p
                style={{
                  fontSize: "32px",
                  fontWeight: "bold",
                  textAlign: "center",
                  marginTop: "50px",
                }}
              >
                "{questionsData[currentIndex].quote}"
              </p>
              <p
                style={{
                  fontSize: "24px",
                  textAlign: "center",
                  marginTop: "20px",
                }}
                id="who-is"
              >
                Did{" "}
                <span id="bin-quote-author" style={{ fontSize: "24px" }}>
                  {randomAuthor.name}
                </span>{" "}
                say this?
              </p>
              {!showResult && (
                <div
                  style={{
                    display: "flex",
                    justifyContent: "center",
                    marginTop: "20%",
                  }}
                >
                  <Button
                    style={{
                      padding: "20px",
                      marginRight: "20px",
                      fontSize: "20px",
                      fontWeight: "bold",
                    }}
                    variant="success"
                    onClick={() =>
                      handleAnswerClick(
                        randomAuthor.id ===
                          questionsData[currentIndex].correctAuthorId
                      )
                    }
                  >
                    Yes
                  </Button>
                  <Button
                    style={{
                      padding: "20px",
                      fontSize: "20px",
                      fontWeight: "bold",
                    }}
                    variant="danger"
                    onClick={() =>
                      handleAnswerClick(
                        randomAuthor.id !==
                          questionsData[currentIndex].correctAuthorId
                      )
                    }
                  >
                    No
                  </Button>
                </div>
              )}
              {showResult && (
                <div>
                  {selectedAnswer ? (
                    <p
                      style={{
                        fontSize: "22px",
                        marginTop: "70px",
                        color: "green",
                        textAlign: "center",
                        fontWeight: "bold",
                      }}
                    >
                      Correct! The answer is:{" "}
                      {randomAuthor.id ===
                      questionsData[currentIndex].correctAuthorId
                        ? "Yes"
                        : "No"}
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
                      Sorry, you are wrong! The correct answer is:{" "}
                      {randomAuthor.id ===
                      questionsData[currentIndex].correctAuthorId
                        ? "Yes"
                        : "No"}
                    </p>
                  )}
                  <Button
                    style={{
                      padding: "20px",
                      fontSize: "20px",
                      fontWeight: "bold",
                      marginTop: "10%",
                      marginLeft: "45%",
                      color: "white",
                    }}
                    variant="info"
                    onClick={handleNextClick}
                  >
                    Next
                  </Button>
                </div>
              )}
            </div>
          )}
        </div>
      )}
    </div>
  );
};

export default BinaryChoiceQuestion;
