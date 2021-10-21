using System;
using System.Collections.Generic;

public class Ultiorganizer
{
    public Match ImportMatch()
    {
        Team[] pickedTeams = PickTeams();
        for (int i = 0; i < pickedTeams.Length; i++)
            pickedTeams[i].players.Sort((p1, p2) => p1.number.CompareTo(p2.number));
        return new Match(DateTime.Now, new TimeSpan(0, 30, 0), new TimeSpan(1, 0, 0), pickedTeams[0], pickedTeams[1]);
    }

    private Team[] PickTeams()
    {
        Team[] allTeams = new Team[] {
        new Team{
        myName = "DiscOver Lisboa",
        players = new List<Player>(){
        new Player{ID = 1, myName = "Ahmed Afify", number = 5, female = false},
        new Player{ID = 2, myName = "Alexander Knoch", number = 67, female = false},
        new Player{ID = 3, myName = "Anna Maryšková", number = 40, female = true},
        new Player{ID = 4, myName = "Christian Hernandez", number = 7, female = false},
        new Player{ID = 5, myName = "Denise Nanni", number = 37, female = true},
        new Player{ID = 6, myName = "Dimitri Rey", number = 83, female = false},
        new Player{ID = 7, myName = "Eduardo Arjona", number = 69, female = false},
        new Player{ID = 8, myName = "Erik Hamre", number = 84, female = false},
        new Player{ID = 9, myName = "Franziska Schranz", number = 55, female = true},
        new Player{ID = 10, myName = "Íris Serôdio", number = 26, female = true},
        new Player{ID = 11, myName = "Jhonny Morales", number = 15, female = false},
        new Player{ID = 12, myName = "Joana Nunes", number = 88, female = true},
        new Player{ID = 13, myName = "José Caetano", number = 48, female = false},
        new Player{ID = 14, myName = "Julian Sonntag", number = 14, female = false},
        new Player{ID = 15, myName = "Maximilian Kohl", number = 0, female = false},
        new Player{ID = 16, myName = "Michaela Primus", number = 44, female = true},
        new Player{ID = 17, myName = "Pedro Pascoal", number = 77, female = false},
        new Player{ID = 18, myName = "Ricardo Fonseca", number = 29, female = false},
        new Player{ID = 19, myName = "Sara Cardoso", number = 16, female = true},
        new Player{ID = 20, myName = "Susana Andrade", number = 47, female = true},
        new Player{ID = 21, myName = "Tereza Řiháčková", number = 99, female = true},
        new Player{ID = 22, myName = "Tomás Ramos", number = 73, female = false}}
    },
        new Team{
        myName = "Gambozinos",
        players = new List<Player>(){
        new Player{ID = 23, myName = "Ana Santos", number = 22, female = true},
        new Player{ID = 24, myName = "Bárbara Marinho", number = 7, female = true},
        new Player{ID = 25, myName = "Bernardo Bem", number = 69, female = false},
        new Player{ID = 26, myName = "Diogo Azevedo", number = 78, female = false},
        new Player{ID = 27, myName = "Dorota Juźków", number = 68, female = true},
        new Player{ID = 28, myName = "Fábio Ferreira", number = 93, female = false},
        new Player{ID = 29, myName = "Fábio Pereira", number = 25, female = false},
        new Player{ID = 30, myName = "Gabriel Marques", number = 16, female = false},
        new Player{ID = 31, myName = "Gareth Baxter", number = 11, female = false},
        new Player{ID = 32, myName = "Joana Cabral", number = 77, female = true},
        new Player{ID = 33, myName = "Joana Soares", number = 76, female = true},
        new Player{ID = 34, myName = "João Alves", number = 74, female = false},
        new Player{ID = 35, myName = "João Simões", number = 75, female = false},
        new Player{ID = 36, myName = "José Seco", number = 55, female = false},
        new Player{ID = 37, myName = "Kamalanathan Ganesan", number = 15, female = false},
        new Player{ID = 38, myName = "Licinio Pereira", number = 12, female = false},
        new Player{ID = 39, myName = "Mariana Barbosa", number = 72, female = true},
        new Player{ID = 40, myName = "Mauro Simões", number = 73, female = false},
        new Player{ID = 41, myName = "Patricia Garrote", number = 9, female = true},
        new Player{ID = 42, myName = "Pedro Domingues", number = 67, female = false},
        new Player{ID = 43, myName = "Sara Ferreira", number = 70, female = true},
        new Player{ID = 44, myName = "Sebastien Lacroix", number = 3, female = false},
        new Player{ID = 45, myName = "Tiago Amaral", number = 91, female = false}}
    },
        new Team{
        myName = "Javalis",
        players = new List<Player>(){
        new Player{ID = 46, myName = "Borbála Kristóf", number = 9, female = true},
        new Player{ID = 47, myName = "Carlota Palha", number = 54, female = true},
        new Player{ID = 48, myName = "Diogo Silva", number = 6, female = false},
        new Player{ID = 49, myName = "Emily Litteral", number = 23, female = true},
        new Player{ID = 50, myName = "Guillermo Gordillo", number = 69, female = false},
        new Player{ID = 51, myName = "Hadar Joseph", number = 73, female = false},
        new Player{ID = 52, myName = "Jan Prazuch", number = 22, female = false},
        new Player{ID = 53, myName = "Jose Lopez", number = 1, female = false},
        new Player{ID = 54, myName = "Juan Garcia", number = 99, female = false},
        new Player{ID = 55, myName = "Kyle Basset", number = 49, female = false},
        new Player{ID = 56, myName = "Miro Marski", number = 32, female = false},
        new Player{ID = 57, myName = "Or Ganani", number = 65, female = false},
        new Player{ID = 58, myName = "Pawel Szymczak", number = 15, female = false},
        new Player{ID = 59, myName = "Rafaela Leal", number = 10, female = true}}
    },
        new Team{
        myName = "LFO Beta",
        players = new List<Player>(){
        new Player{ID = 60, myName = "André Rocha", number = 0, female = false},
        new Player{ID = 61, myName = "Bernardo Trindade", number = 93, female = false},
        new Player{ID = 62, myName = "Dina Rodrigues", number = 77, female = true},
        new Player{ID = 63, myName = "Georgina Lourenço", number = 53, female = true},
        new Player{ID = 64, myName = "Isabel Santos", number = 39, female = true},
        new Player{ID = 65, myName = "João Botas", number = 70, female = false},
        new Player{ID = 66, myName = "João Lopes", number = 10, female = false},
        new Player{ID = 67, myName = "Luis Pinhal", number = 44, female = false},
        new Player{ID = 68, myName = "Marco Nave", number = 1, female = false},
        new Player{ID = 69, myName = "Nuno Silva", number = 40, female = false},
        new Player{ID = 70, myName = "Patrícia Nunes", number = 98, female = true},
        new Player{ID = 71, myName = "Paula Norte", number = 4, female = true},
        new Player{ID = 72, myName = "Raquel Santos", number = 16, female = true},
        new Player{ID = 73, myName = "Rui Silva", number = 95, female = false},
        new Player{ID = 74, myName = "Sofia Silva", number = 36, female = true}}
    },
        new Team{
        myName = "LFO Premium",
        players = new List<Player>(){
        new Player{ID = 75, myName = "Ana Chaves", number = 17, female = true},
        new Player{ID = 76, myName = "Ana Oliveira", number = 12, female = true},
        new Player{ID = 77, myName = "Ana Poim", number = 94, female = true},
        new Player{ID = 78, myName = "Bruno Tribovane", number = 72, female = false},
        new Player{ID = 79, myName = "Daniel Assunção", number = 25, female = false},
        new Player{ID = 80, myName = "Diogo Bagagem", number = 91, female = false},
        new Player{ID = 81, myName = "Frederica Biel", number = 19, female = true},
        new Player{ID = 82, myName = "Martim Santos", number = 92, female = false},
        new Player{ID = 83, myName = "Nelson Antunes", number = 21, female = false},
        new Player{ID = 84, myName = "Nuno Alves", number = 89, female = false},
        new Player{ID = 85, myName = "Patrícia Amoroso", number = 37, female = true},
        new Player{ID = 86, myName = "Pedro Assunção", number = 52, female = false},
        new Player{ID = 87, myName = "Ricardo Godinho", number = 7, female = false},
        new Player{ID = 88, myName = "Rita Chaves", number = 27, female = true},
        new Player{ID = 89, myName = "Telma Lopes", number = 51, female = true}}
    },
        new Team{
        myName = "UFA",
        players = new List<Player>(){
        new Player{ID = 90, myName = "Afonso Coelho", number = 1, female = false},
        new Player{ID = 91, myName = "Ana Duarte", number = 18, female = true},
        new Player{ID = 92, myName = "Bruna Almeida", number = 16, female = true},
        new Player{ID = 93, myName = "Christian Baez", number = 99, female = false},
        new Player{ID = 94, myName = "Francisco Patrício", number = 62, female = false},
        new Player{ID = 95, myName = "Inês Bringel", number = 7, female = true},
        new Player{ID = 96, myName = "Mariana Inácio", number = 10, female = true},
        new Player{ID = 97, myName = "Rui João", number = 86, female = false},
        new Player{ID = 98, myName = "Rui Nave", number = 4, female = false},
        new Player{ID = 99, myName = "Simão Tempera", number = 42, female = false},
        new Player{ID = 100, myName = "Tiago Augusto", number = 11, female = false},
        new Player{ID = 101, myName = "Tiago Tempera", number = 0, female = false}}
    },
        new Team{
        myName = "Vira o Disco",
        players = new List<Player>(){
        new Player{ID = 102, myName = "Constança Simas", number = 35, female = true},
        new Player{ID = 103, myName = "Corinne Tupling", number = 2, female = true},
        new Player{ID = 104, myName = "David Lemos", number = 6, female = false},
        new Player{ID = 105, myName = "Filipa May", number = 29, female = true},
        new Player{ID = 106, myName = "Humberto Rodrigues", number = 17, female = false},
        new Player{ID = 107, myName = "Inês Xavier", number = 21, female = true},
        new Player{ID = 108, myName = "Kendall Ferreira", number = 28, female = true},
        new Player{ID = 109, myName = "Luís Conceição", number = 3, female = false},
        new Player{ID = 110, myName = "Maria Fonseca", number = 13, female = true},
        new Player{ID = 111, myName = "Matthew Tupling", number = 1, female = false},
        new Player{ID = 112, myName = "Pedro", number = 31, female = false},
        new Player{ID = 113, myName = "Rafael Pifre", number = 0, female = false},
        new Player{ID = 114, myName = "Rui Ferreira", number = 12, female = false},
        new Player{ID = 115, myName = "Sara Costa", number = 8, female = true},
        new Player{ID = 116, myName = "Vanessa Eterno", number = 99, female = true}}
    }
    };

        Team[] currentTeams = new Team[2];
        int r1 = UnityEngine.Random.Range(0, allTeams.Length);
        int r2;
        currentTeams[0] = allTeams[r1];
        do
        {
            r2 = UnityEngine.Random.Range(0, allTeams.Length);
        } while (r1 == r2);
        currentTeams[1] = allTeams[r2];

        return currentTeams;
    }
}