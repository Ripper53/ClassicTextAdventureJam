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
                    yield return "BUTTON";
                    yield return "SCREEN";
                    yield return "DISPLAY";
                    yield return "LABEL";
                }
            }

            private readonly List<Question> questions = new List<Question> {
                new StrictQuestion("What’s the fastest speed one can travel?", "The speed of light", "Teleportation", "The answer is: the speed of light!", "SPEED OF LIGHT", "LIGHT SPEED", "LIGHTSPEED", "LIGHT-SPEED"),
                new DynamicQuestion("What 3-digit number is divisible by 32?", "256!", "255!", "256, 128, or 512 were possible answers!", ans => ans.Length == 3 && int.TryParse(ans, out int r) && (r % 32) == 0),
                new StrictQuestion("In General Relativity, what is gravity the equivalent of?", "Acceleration", "Velocity", "Gravity is equivalent to acceleration!", "ACCELERATION")
            };

            private void Correct(string name) {
                if (rng.NextDouble() < 0.5f)
                    Act.WriteLine(@"""That is correct!""");
                else
                    Act.WriteLine($@"""Point goes to {name}!""");
            }
            private void Incorrect(Question question, string name) {
                if (rng.NextDouble() < 0.5f)
                    Act.WriteLine($@"""That is incorrect! {question.RevealAnswer}""");
                else
                    Act.WriteLine($@"""{name} has lost a point! {question.RevealAnswer}""");
            }

            public void Execute(IWork.Token token) {
                Act.WriteLine(@"""Both contestants are ready!"" yells the voice. The crowd eagerly awaits the beginning of the contest. ""Three,"" says the figure. ""3"" displays the screen. ""Two,"" says the figure. ""2"" displays the screen. ""One,"" says the figure. ""1"" displays the screen. The figure raises its hand and points up. ""Let’s begin!""");

                int playerScore = 0, opponentScore = 0;
                Act.AsyncConsole console = new Act.AsyncConsole();
                Question question;
                string answer;
                System.Threading.Thread.Sleep(12000);
                while (questions.Count > 0) {
                    int index = rng.Next(0, questions.Count);
                    question = questions[index];
                    Act.WriteLine(question.Value);
                    questions.RemoveAt(index);
                    if (ReadInputBefore(console, question, out answer)) {
                        // PLAYER ANSWERED!
                        if (question.CheckAnswer(answer.ToUpper())) {
                            playerScore += 1;
                            Correct(StaticData.PlayerName);
                        } else {
                            playerScore -= 1;
                            Incorrect(question, StaticData.PlayerName);
                        }
                    } else if (question.OpponentCorrect) {
                        opponentScore += 1;
                        // Opponent answered correctly!
                        Correct(StaticData.OpponentName);
                    } else {
                        opponentScore -= 1;
                        // Opponent answered incorrectly!
                        Incorrect(question, StaticData.OpponentName);
                    }
                }

                if (playerScore > opponentScore) {
                    Act.WriteLine($@"""We have a winner!"" the figure points to you, and the crowd rallies! {StaticData.OpponentName} drops into a trapdoor and your platform begins to rise above the crowd.

Now you see, the spotlight upon the ceiling was the eye, burning like a star. You ascend closer and closer towards it, and your skin begins to burn and incinerate as you turn to ash.

You awake from a sleeplike state and see your body morphed into a radiating transcendental figure. Everywhere you look, all the light from the direction directs towards your sight and you witness every entity along your vision. Every star, blackhole, alien, machine, and other floating figures. Larger than a single individual, inhabiting an immense amount of energy within you, you have no concern for struggle or war. Now, with your clairvoyance, you seek to bring others towards a similar fate.");
                } else {
                    Act.WriteLine($@"""We have a winner!"" the figure points to {StaticData.OpponentName}. You drop into a trapdoor leading straight to the Control Room.

Through the large window, you see a faint orange glow in the distance. The watchful eye vanished from space like it were never there. ""The {StaticData.OpponentShipName} has been eliminated!"" shouts the radio in excitement, and you realize the orange is emitting from the destruction of the {StaticData.OpponentShipName}. ""And the millennial long war has come to an end! The {StaticData.PlayerShipName} and we, its supporters, are victorious!""");
                }

                Act.WriteTitle("THE END");

                Act.ReadLine();
                token.GameManager.Quit();

            }
        }
        #region Question
        private abstract class Question {
            public string Value { get; }
            public string RightAnswer { get; }
            public string WrongAnswer { get; }
            public string RevealAnswer { get; }
            public Question(string value, string rightAnswer, string wrongAnswer, string revealAnswer) {
                Value = value;
                RightAnswer = rightAnswer;
                WrongAnswer = wrongAnswer;
                RevealAnswer = revealAnswer;
            }

            public abstract bool CheckAnswer(string answer);

            public bool OpponentCorrect { get; set; } = false;

        }
        private class StrictQuestion : Question {
            public IEnumerable<string> Contains { get; }
            public StrictQuestion(string value, string rightAnswer, string wrongAnswer, string revealAnswer, params string[] contains) : base(value, rightAnswer, wrongAnswer, revealAnswer) {
                Contains = contains;
            }

            public override bool CheckAnswer(string answer) {
                foreach (string contain in Contains) {
                    if (answer.Contains(contain)) {
                        // CORRECT!
                        return true;
                    }
                }
                return false;
            }

        }
        private class DynamicQuestion : Question {
            public delegate bool CheckAnswerFunc(string answer);
            private readonly CheckAnswerFunc func;
            public DynamicQuestion(string value, string rightAnswer, string wrongAnswer, string revealAnswer, CheckAnswerFunc func) : base(value, rightAnswer, wrongAnswer, revealAnswer) {
                this.func = func;
            }
            public override bool CheckAnswer(string answer) => func(answer);
        }
        #endregion

        private static bool ReadInputBefore(Act.AsyncConsole console, Question question, out string answer) {
            bool cancel = false;
            var task = Task.Run(() => {
                // your task on separate thread
                Timer timer = new Timer();
                timer.Interval = rng.Next(6000, 12001);
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
                    question.OpponentCorrect = true;
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
