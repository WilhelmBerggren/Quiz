using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Data
{
    public class DbInitializer
    {
        public static void Initialize(QuizContext context)
        {
            context.Database.EnsureCreated();

            if (context.QuizItems.Any())
            {
                return;
            }

            var quizItems = new QuizItem[]
            {
                new QuizItem {Title = "Is Lorem Ipsum the industry's standard dummy text?",
                Answer = "Yes",
                Option1 = "No",
                Option2 = "Lorem ipsum dolor sit amet"},

                new QuizItem {Title = "Why?",
                Answer = "I don't know man, I just came for the pizza",
                Option1 = "Yeet",
                Option2 = "This is the right answer"},

                new QuizItem {Title = "Have you tried Vegemite?",
                Answer = "Oi mate, I got that vegemite stuck in me peen",
                Option1 = "Vegemite is just up-side-down Marmite",
                Option2 = "I svea rike använder vi enbart Tartex och smörgåsgurka"},
                
                new QuizItem {Title = "PK fire?",
                Answer = "PK FIRE! PK FIRE! PK FIRE! PK FIRE!",
                Option1 = "Backthrow needs a buff",
                Option2 = "PK Thunder is a great recovery"},

                new QuizItem {Title = "How much wood would a woodchuck chuck if a woodchuck could feel happyness?",
                Answer = "80 painful years",
                Option1 = "null",
                Option2 = "NaN"},

                new QuizItem {Title = "Does thou skrrt skrrt?",
                Answer = "Yes but on the inside I hurt hurt",
                Option1 = "Big Shaq boom boom",
                Option2 = "lmao ye man"},

                new QuizItem {Title = "What's your story",
                Answer = "...",
                Option1 = "I was finally awake! I tried to cross the border and walked right into that imperial ambush same as that thief over there.",
                Option2 = "At age six I was born without a face."},

                new QuizItem {Title = "Hello there!",
                Answer = "General Kenobi!",
                Option1 = "I came to chew ass and kick bubblegum",
                Option2 = "Oj! Vilken överaskning! Kommer och filmar mig när jag går på muggen!"},

                new QuizItem {Title = "What in Oblivion is that!?",
                Answer = "a footlong Dragon Dildo",
                Option1 = "Crocs",
                Option2 = "My will to live"},

                new QuizItem {Title = "What have we got to lose?",
                Answer = "Nothing but our chains",
                Option1 = "Deez nutz",
                Option2 = "seething hatred for the muffinman"}
            };

            context.AddRange(quizItems);
            context.SaveChanges();

            if (context.ScoreItems.Any())
            {
                return;
            }

            var scoreItems = new ScoreItem[]
            {
                new ScoreItem() {
                    User = "Gandhi",
                    Score = 8
                },

                new ScoreItem() {
                    User = "Masahiro Sakurai",
                    Score = 2
                },

                new ScoreItem() {
                    User = "Karlsson på Taket",
                    Score = 0
                },

                new ScoreItem() {
                    User = "asdfmannen",
                    Score = 5
                },

                new ScoreItem() {
                    User = "Min boii Xi Jin Ping",
                    Score = 12
                },
            };

            context.ScoreItems.AddRange(scoreItems);
            context.SaveChanges();
        }
    }
}
