using System;
using System.Collections.Generic;
using System.Text;

namespace OthelloLogic
{
    public class gameManager
    {
        private Board m_Board;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private int m_WhichPlayerPlay = 1;
        private amoutOfPlayerWithNoTurn m_CounterAmountOfPlayerWithNoTurn = amoutOfPlayerWithNoTurn.ZeroPlayer;

        public string[,] StartGame(int i_HeightAndWidth)
        {
            m_Board = new Board();
            return m_Board.GetStartingBoard(i_HeightAndWidth);
        }

        public List<Utilities.ValidStep> GetAllValidSteps(char i_PlayerShape)
        {
            return m_Board.getAllValidSteps(i_PlayerShape);
        }

        public void SetPlayerOnGameManager(string i_playerName, bool i_isHuman, char i_shape, int i_playerNumber)
        {
            if (i_playerNumber == 1)
            {
                m_PlayerOne = new Player(i_playerName, i_isHuman, i_shape);
            }
            else
            {
                m_PlayerTwo = new Player(i_playerName, i_isHuman, i_shape);
            }
        }

        public Utilities.checkNextStepForTheProgram CheckVaildStepAndUpdateBoard(Utilities.Point i_PointToCheck)
        {
            if (m_WhichPlayerPlay == 1)
            {
                if (!tryDoHumanTurn(i_PointToCheck, m_PlayerOne))
                {
                    if (m_CounterAmountOfPlayerWithNoTurn == amoutOfPlayerWithNoTurn.TwoPlayer)
                    {
                        return Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay;
                    }

                    return Utilities.checkNextStepForTheProgram.IllegalStep;
                }

                if (!m_PlayerTwo.isAHuman())
                {
                    if (m_CounterAmountOfPlayerWithNoTurn == amoutOfPlayerWithNoTurn.TwoPlayer)
                    {
                        return Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay;
                    }

                    tryDoComputerTurn();
                    m_WhichPlayerPlay = 1;
                }
                else
                {
                    m_WhichPlayerPlay = 2;
                }

                return Utilities.checkNextStepForTheProgram.ValidStep;
            }
            else
            {
                if (!tryDoHumanTurn(i_PointToCheck, m_PlayerTwo))
                {
                    if (m_CounterAmountOfPlayerWithNoTurn == amoutOfPlayerWithNoTurn.TwoPlayer)
                    {
                        return Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay;
                    }
                    return Utilities.checkNextStepForTheProgram.IllegalStep;
                }
                else
                {
                    m_WhichPlayerPlay = 1;
                }
                return Utilities.checkNextStepForTheProgram.ValidStep;
            }
        }

        private bool isStepIsValid(List<Utilities.ValidStep> i_ValidSteps, Utilities.Point i_PointToCheck)
        {

            int sizeOfList = i_ValidSteps.Count;
            Utilities.ValidStep[] CloneValidSteps = new Utilities.ValidStep[sizeOfList];
            i_ValidSteps.CopyTo((CloneValidSteps));

            foreach (Utilities.ValidStep currentStepToCheck in CloneValidSteps)
            {
                if (!(currentStepToCheck.GetPoint().X() == i_PointToCheck.X() && currentStepToCheck.GetPoint().Y() == i_PointToCheck.Y()))
                {
                    i_ValidSteps.Remove(currentStepToCheck);
                }
            }

            return i_ValidSteps.Count > 0;
        }

        private bool isListEmpty(List<Utilities.ValidStep> i_validSteps)
        {
            return i_validSteps.Count == 0;
        }

        private void tryDoComputerTurn()
        {
            List<Utilities.ValidStep> validSteps;
            if (isTurnPossible(out validSteps, m_PlayerTwo))
            {
                m_Board.ReduceBlankCells();
                m_CounterAmountOfPlayerWithNoTurn = amoutOfPlayerWithNoTurn.ZeroPlayer;
                int amountOfValidSteps = validSteps.Count;

                Random randomPlay = new Random();
                int randomStepIndex = randomPlay.Next(0, amountOfValidSteps);

                m_Board.updateBoard(validSteps[randomStepIndex], m_PlayerTwo.getShape().ToString());
            }
            else
            {
                updateAmountOfPlayerThatDontPlay();
            }
        }

        public void UpdateBoard(Utilities.ValidStep i_ValidStep, string i_PlayerShape)
        {
            m_Board.updateBoard(i_ValidStep, i_PlayerShape);
        }

        private bool tryDoHumanTurn(Utilities.Point i_PointToCheck, Player i_PlayerToPlay)
        {
            List<Utilities.ValidStep> validSteps;

            if ((isTurnPossible(out validSteps, i_PlayerToPlay)))
            {
                m_Board.ReduceBlankCells();
                m_CounterAmountOfPlayerWithNoTurn = amoutOfPlayerWithNoTurn.ZeroPlayer;

                if ((isStepIsValid(validSteps, i_PointToCheck)))
                {
                    m_Board.updateBoard(validSteps[0], i_PlayerToPlay.getShape().ToString());
                    return true;
                }
            }
            else
            {
                updateAmountOfPlayerThatDontPlay();
            }

            return false;
        }

        private void updateAmountOfPlayerThatDontPlay()
        {
            if (m_CounterAmountOfPlayerWithNoTurn == amoutOfPlayerWithNoTurn.ZeroPlayer)
            {
                m_CounterAmountOfPlayerWithNoTurn = amoutOfPlayerWithNoTurn.OnePlyer;
            }
            else
            {
                m_CounterAmountOfPlayerWithNoTurn = amoutOfPlayerWithNoTurn.TwoPlayer;
            }
        }

        public bool isTurnPossible(out List<Utilities.ValidStep> i_ValidSteps, Player i_PlayerToPlay)
        {
            i_ValidSteps = m_Board.getAllValidSteps(i_PlayerToPlay.getShape());
            int amountOfValidSteps = i_ValidSteps.Count;

            return amountOfValidSteps != 0;
        }

        public bool isTurnPossible(out List<Utilities.ValidStep> i_ValidSteps, char i_PlayerShape)
        {///overLoadFunction added
            i_ValidSteps = m_Board.getAllValidSteps(i_PlayerShape);
            int amountOfValidSteps = i_ValidSteps.Count;

            return amountOfValidSteps != 0;
        }

        public bool CheckIfGameIsOver()
        {
            List<Utilities.ValidStep> validStepsPlayerOne;
            List<Utilities.ValidStep> validStepsPlayerTwo;

            //if (!isTurnPossible(out validStepsPlayerOne, m_PlayerOne))
            //{
            //    m_WhichPlayerPlay = 2;
            //    if (!m_PlayerTwo.isAHuman())
            //    {
            //        CheckVaildStepAndUpdateBoard(new Utilities.Point(0, 0));
            //    }
            //}

            return (!isTurnPossible(out validStepsPlayerOne, m_PlayerOne)
                && (!isTurnPossible(out validStepsPlayerTwo, m_PlayerTwo)));
        }

        public Utilities.checkNextStepForTheProgram CheckfBoardFullOrNoMoreMovesForPlayers()
        {
            if (m_Board.IsBoardFull())
            {
                return Utilities.checkNextStepForTheProgram.EndGame;
            }

            return Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay;
        }

        public string GetNameOfThePlayerTurn()
        {
            if (m_WhichPlayerPlay == 1)
            {
                return GetNamePlayerOne();
            }
            else
            {
                return GetNamePlayerTwo();
            }
        }

        public bool IsThePlayerComputer(char i_Shape)
        {
            bool result;
            if (i_Shape == m_PlayerOne.getShape())
            {
                result = !m_PlayerOne.isAHuman();
            }
            else
            {
                result = !m_PlayerTwo.isAHuman();
            }

            return result;
        }

        public string GetNamePlayerOne()
        {
            return m_PlayerOne.getName();
        }

        public string GetNamePlayerTwo()
        {
            return m_PlayerTwo.getName();
        }

        public void GetScore(out int i_PlayerOneScore, out int i_PlayerTwoScore)
        {
            m_Board.GetScoreToPlayer(out i_PlayerOneScore, m_PlayerOne.getShape(), out i_PlayerTwoScore, m_PlayerTwo.getShape());
        }

        public Utilities.checkNextStepForTheProgram IsTheGameContinue()
        {
            if (!m_Board.IsHaveBlankCells())
            {
                return Utilities.checkNextStepForTheProgram.EndGame;
            }

            if (CheckIfGameIsOver())
            {
                return Utilities.checkNextStepForTheProgram.TwoPlayerCantPlay;
            }

            return Utilities.checkNextStepForTheProgram.Run;
        }

        private enum amoutOfPlayerWithNoTurn
        {
            TwoPlayer,
            OnePlyer,
            ZeroPlayer
        };
    }
}
