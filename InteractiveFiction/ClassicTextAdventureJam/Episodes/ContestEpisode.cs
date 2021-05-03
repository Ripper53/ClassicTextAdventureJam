using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using EsotericFiction;

namespace ClassicTextAdventureJam.Episodes {
    public class ContestEpisode : IEpisode {
        private static readonly Random rng = new Random();

        public void Play(IEpisode.Token token) {
            Act.WriteLine(@"The screen displays a button labeled ""Begin.""");
            token.ActiveScene.AddWork(new BeginWork());
        }

        private class BeginWork : IWork {
            public IEnumerable<string> Action {
                get {
                    yield return "PRESS";
                    yield return "CLICK";
                    yield return "TAP";
                }
            }
            public IEnumerable<string> Upon {
                get {
                    yield return "BEGIN";
                }
            }

            private readonly List<Question> questions = new List<Question> {
                new Question("What’s the fastest speed one can travel?", "The speed of light", "Teleportation", "SPEED OF LIGHT", "LIGHT SPEED", "LIGHTSPEED"),
                new Question("", "", "", ""),
                new Question("", "", "", ""),
                new Question("", "", "", ""),
                new Question("", "", "", "")
            };

            public void Execute(IWork.Token token) {
                Act.WriteLine(@"""Both contestants are ready!"" yells the voice. The crowd eagerly awaits the beginning of the contest. ""Three,"" says the figure. ""3"" displays the screen. ""Two,"" says the figure. ""2"" displays the screen. ""One,"" says the figure. ""1"" displays the screen. The figure raises its hand and points up. ""Let’s begin!""

""We’ll start with an easy one. What’s the fastest speed one can travel?""");

                Act.AsyncConsole console = new Act.AsyncConsole();
                Question question;
                string answer;
                bool CheckIfCorrect() {
                    foreach (string contain in question.Contains) {
                        if (answer.Contains(contain)) {
                            // CORRECT!
                            Act.WriteLine(@"""Correct!""");
                            return true;
                        }
                    }
                    return false;
                }
                while (questions.Count > 0) {
                    int index = rng.Next(0, questions.Count);
                    question = questions[index];
                    questions.RemoveAt(index);
                    if (ReadInputBefore(console, question, out answer)) {
                        // PLAYER ANSWERED!
                        answer = answer.ToUpper();
                        if (!CheckIfCorrect()) // INCORRECT!
                            Act.WriteLine(@"""Incorrect!""");
                    } else if (question.Correct) {
                        // Opponent answered correctly!
                        Act.WriteLine(@"""Correct!""");
                    } else {
                        // Opponent answered incorrectly!
                        Act.WriteLine(@"""Incorrect!""");
                    }
                }
            }
        }
        private class Question {
            public string Value { get; }
            public string RightAnswer { get; }
            public string WrongAnswer { get; }
            public IEnumerable<string> Contains { get; }
            public Question(string value, string rightAnswer, string wrongAnswer, params string[] contains) {
                Value = value;
                RightAnswer = rightAnswer;
                WrongAnswer = wrongAnswer;
                Contains = contains;
            }

            public bool Correct { get; set; } = false;

        }

        private static bool ReadInputBefore(Act.AsyncConsole console, Question question, out string answer) {
            bool cancel = false;
            var task = Task.Run(() => {
                // your task on separate thread
                Timer timer = new Timer();
                timer.Interval = rng.Next(3000, 6001);
                bool done = false;
                timer.Elapsed += (sender, e) => done = true;
                timer.Start();
                while (!done && !cancel) { }
            });

            Task<string> input = console.ReadLine();
            if (Task.WaitAny(input, task) == 0) {
                cancel = true;
                // if ReadLine finished first
                answer = input.Result; // last user input (await instead of Result in async method)
                return true;
            } else {
                // task finished first
                //var x = console.ReadLine(); // this wont issue another read line because user did not input anything yet.
                if (rng.NextDouble() < 0.5) {
                    // RIGHT
                    question.Correct = true;
                    answer = question.RightAnswer;
                } else {
                    // WRONG
                    answer = question.WrongAnswer;
                }
                Act.WriteLine($@"{Environment.NewLine}""{answer},"" said {StaticData.OpponentName}.");
                return false;
            }
        }

    }
}
