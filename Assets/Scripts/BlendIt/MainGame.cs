using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainGame
{
    public static Dictionary<Dificulty, List<string>> dificultyWords = new Dictionary<Dificulty, List<string>>()
        {
            {
                Dificulty.Easy, new List<string>()
                {
                    "agua",
                    "maca",
                    "pera",
                    "uva",
                    "leite",
                    "pao",
                    "ovo",
                    "sopa",
                    "arroz",
                    "peixe",
                    "milho",
                    "banana",
                    "tomate",
                    "cenoura",
                    "alface",
                    "queijo",
                    "iogurte",
                    "laranja",
                    "morango",
                    "batata"
                }
            },
            {
                Dificulty.Moderate, new List<string>()
                {
                    "salada",
                    "legumes",
                    "fruta",
                    "feijao",
                    "aveia",
                    "cereais",
                    "frango",
                    "brocolos",
                    "melancia",
                    "abobora",
                    "ervilhas",
                    "pepino",
                    "massa",
                    "espinafre",
                    "natural",
                    "saudavel",
                    "refeicao",
                    "vitamina",
                    "energia",
                    "lanche"
                }
            },
            {
                Dificulty.Hard, new List<string>()
                {
                    "nutrientes",
                    "proteina",
                    "integral",
                    "sementes",
                    "castanhas",
                    "amendoim",
                    "tangerina",
                    "framboesa",
                    "couveflor",
                    "beterraba",
                    "batatadoce",
                    "cozinhar",
                    "mastigar",
                    "crescer",
                    "colorido",
                    "equilibrado",
                    "sopaquente",
                    "graodebico",
                    "frutossecos",
                    "sumonatural"
                }
            }
        };

    public enum Dificulty
    {
        Easy,
        Moderate,
        Hard
    }

    public static Dificulty currentDificulty = Dificulty.Moderate;

    public void Reset()
    {
        currentDificulty = Dificulty.Moderate;
    }

}
