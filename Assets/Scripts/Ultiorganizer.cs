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
        new Player{ID = 1, fullName = "Ahmed Afify", number = 5, female = false},
        new Player{ID = 2, fullName = "Alexander Knoch", number = 67, female = false},
        new Player{ID = 3, fullName = "Anna Maryšková", number = 40, female = true},
        new Player{ID = 4, fullName = "Christian Hernandez", number = 7, female = false},
        new Player{ID = 5, fullName = "Denise Nanni", number = 37, female = true},
        new Player{ID = 6, fullName = "Dimitri Rey", number = 83, female = false},
        new Player{ID = 7, fullName = "Eduardo Arjona", number = 69, female = false},
        new Player{ID = 8, fullName = "Erik Hamre", number = 84, female = false},
        new Player{ID = 9, fullName = "Franziska Schranz", number = 55, female = true},
        new Player{ID = 10, fullName = "Íris Serôdio", number = 26, female = true},
        new Player{ID = 11, fullName = "Jhonny Morales", number = 15, female = false},
        new Player{ID = 12, fullName = "Joana Nunes", number = 88, female = true},
        new Player{ID = 13, fullName = "José Caetano", number = 48, female = false},
        new Player{ID = 14, fullName = "Julian Sonntag", number = 14, female = false},
        new Player{ID = 15, fullName = "Maximilian Kohl", number = 0, female = false},
        new Player{ID = 16, fullName = "Michaela Primus", number = 44, female = true},
        new Player{ID = 17, fullName = "Pedro Pascoal", number = 77, female = false},
        new Player{ID = 18, fullName = "Ricardo Fonseca", number = 29, female = false},
        new Player{ID = 19, fullName = "Sara Cardoso", number = 16, female = true},
        new Player{ID = 20, fullName = "Susana Andrade", number = 47, female = true},
        new Player{ID = 21, fullName = "Tereza Řiháčková", number = 99, female = true},
        new Player{ID = 22, fullName = "Tomás Ramos", number = 73, female = false}}
    },
        new Team{
        myName = "Gambozinos",
        players = new List<Player>(){
        new Player{ID = 23, fullName = "Ana Santos", number = 22, female = true},
        new Player{ID = 24, fullName = "Bárbara Marinho", number = 7, female = true},
        new Player{ID = 25, fullName = "Bernardo Bem", number = 69, female = false},
        new Player{ID = 26, fullName = "Diogo Azevedo", number = 78, female = false},
        new Player{ID = 27, fullName = "Dorota Juźków", number = 68, female = true},
        new Player{ID = 28, fullName = "Fábio Ferreira", number = 93, female = false},
        new Player{ID = 29, fullName = "Fábio Pereira", number = 25, female = false},
        new Player{ID = 30, fullName = "Gabriel Marques", number = 16, female = false},
        new Player{ID = 31, fullName = "Gareth Baxter", number = 11, female = false},
        new Player{ID = 32, fullName = "Joana Cabral", number = 77, female = true},
        new Player{ID = 33, fullName = "Joana Soares", number = 76, female = true},
        new Player{ID = 34, fullName = "João Alves", number = 74, female = false},
        new Player{ID = 35, fullName = "João Simões", number = 75, female = false},
        new Player{ID = 36, fullName = "José Seco", number = 55, female = false},
        new Player{ID = 37, fullName = "Kamalanathan Ganesan", number = 15, female = false},
        new Player{ID = 38, fullName = "Licinio Pereira", number = 12, female = false},
        new Player{ID = 39, fullName = "Mariana Barbosa", number = 72, female = true},
        new Player{ID = 40, fullName = "Mauro Simões", number = 73, female = false},
        new Player{ID = 41, fullName = "Patricia Garrote", number = 9, female = true},
        new Player{ID = 42, fullName = "Pedro Domingues", number = 67, female = false},
        new Player{ID = 43, fullName = "Sara Ferreira", number = 70, female = true},
        new Player{ID = 44, fullName = "Sebastien Lacroix", number = 3, female = false},
        new Player{ID = 45, fullName = "Tiago Amaral", number = 91, female = false}}
    },
        new Team{
        myName = "Javalis",
        players = new List<Player>(){
        new Player{ID = 46, fullName = "Borbála Kristóf", number = 9, female = true},
        new Player{ID = 47, fullName = "Carlota Palha", number = 54, female = true},
        new Player{ID = 48, fullName = "Diogo Silva", number = 6, female = false},
        new Player{ID = 49, fullName = "Emily Litteral", number = 23, female = true},
        new Player{ID = 50, fullName = "Guillermo Gordillo", number = 69, female = false},
        new Player{ID = 51, fullName = "Hadar Joseph", number = 73, female = false},
        new Player{ID = 52, fullName = "Jan Prazuch", number = 22, female = false},
        new Player{ID = 53, fullName = "Jose Lopez", number = 1, female = false},
        new Player{ID = 54, fullName = "Juan Garcia", number = 99, female = false},
        new Player{ID = 55, fullName = "Kyle Basset", number = 49, female = false},
        new Player{ID = 56, fullName = "Miro Marski", number = 32, female = false},
        new Player{ID = 57, fullName = "Or Ganani", number = 65, female = false},
        new Player{ID = 58, fullName = "Pawel Szymczak", number = 15, female = false},
        new Player{ID = 59, fullName = "Rafaela Leal", number = 10, female = true}}
    },
        new Team{
        myName = "LFO Beta",
        players = new List<Player>(){
        new Player{ID = 60, fullName = "André Rocha", number = 0, female = false},
        new Player{ID = 61, fullName = "Bernardo Trindade", number = 93, female = false},
        new Player{ID = 62, fullName = "Dina Rodrigues", number = 77, female = true},
        new Player{ID = 63, fullName = "Georgina Lourenço", number = 53, female = true},
        new Player{ID = 64, fullName = "Isabel Santos", number = 39, female = true},
        new Player{ID = 65, fullName = "João Botas", number = 70, female = false},
        new Player{ID = 66, fullName = "João Lopes", number = 10, female = false},
        new Player{ID = 67, fullName = "Luis Pinhal", number = 44, female = false},
        new Player{ID = 68, fullName = "Marco Nave", number = 1, female = false},
        new Player{ID = 69, fullName = "Nuno Silva", number = 40, female = false},
        new Player{ID = 70, fullName = "Patrícia Nunes", number = 98, female = true},
        new Player{ID = 71, fullName = "Paula Norte", number = 4, female = true},
        new Player{ID = 72, fullName = "Raquel Santos", number = 16, female = true},
        new Player{ID = 73, fullName = "Rui Silva", number = 95, female = false},
        new Player{ID = 74, fullName = "Sofia Silva", number = 36, female = true}}
    },
        new Team{
        myName = "LFO Premium",
        players = new List<Player>(){
        new Player{ID = 75, fullName = "Ana Chaves", number = 17, female = true},
        new Player{ID = 76, fullName = "Ana Oliveira", number = 12, female = true},
        new Player{ID = 77, fullName = "Ana Poim", number = 94, female = true},
        new Player{ID = 78, fullName = "Bruno Tribovane", number = 72, female = false},
        new Player{ID = 79, fullName = "Daniel Assunção", number = 25, female = false},
        new Player{ID = 80, fullName = "Diogo Bagagem", number = 91, female = false},
        new Player{ID = 81, fullName = "Frederica Biel", number = 19, female = true},
        new Player{ID = 82, fullName = "Martim Santos", number = 92, female = false},
        new Player{ID = 83, fullName = "Nelson Antunes", number = 21, female = false},
        new Player{ID = 84, fullName = "Nuno Alves", number = 89, female = false},
        new Player{ID = 85, fullName = "Patrícia Amoroso", number = 37, female = true},
        new Player{ID = 86, fullName = "Pedro Assunção", number = 52, female = false},
        new Player{ID = 87, fullName = "Ricardo Godinho", number = 7, female = false},
        new Player{ID = 88, fullName = "Rita Chaves", number = 27, female = true},
        new Player{ID = 89, fullName = "Telma Lopes", number = 51, female = true}}
    },
        new Team{
        myName = "UFA",
        players = new List<Player>(){
        new Player{ID = 90, fullName = "Afonso Coelho", number = 1, female = false},
        new Player{ID = 91, fullName = "Ana Duarte", number = 18, female = true},
        new Player{ID = 92, fullName = "Bruna Almeida", number = 16, female = true},
        new Player{ID = 93, fullName = "Christian Baez", number = 99, female = false},
        new Player{ID = 94, fullName = "Francisco Patrício", number = 62, female = false},
        new Player{ID = 95, fullName = "Inês Bringel", number = 7, female = true},
        new Player{ID = 96, fullName = "Mariana Inácio", number = 10, female = true},
        new Player{ID = 97, fullName = "Rui João", number = 86, female = false},
        new Player{ID = 98, fullName = "Rui Nave", number = 4, female = false},
        new Player{ID = 99, fullName = "Simão Tempera", number = 42, female = false},
        new Player{ID = 100, fullName = "Tiago Augusto", number = 11, female = false},
        new Player{ID = 101, fullName = "Tiago Tempera", number = 0, female = false}}
    },
        new Team{
        myName = "Vira o Disco",
        players = new List<Player>(){
        new Player{ID = 102, fullName = "Constança Simas", number = 35, female = true},
        new Player{ID = 103, fullName = "Corinne Tupling", number = 2, female = true},
        new Player{ID = 104, fullName = "David Lemos", number = 6, female = false},
        new Player{ID = 105, fullName = "Filipa May", number = 29, female = true},
        new Player{ID = 106, fullName = "Humberto Rodrigues", number = 17, female = false},
        new Player{ID = 107, fullName = "Inês Xavier", number = 21, female = true},
        new Player{ID = 108, fullName = "Kendall Ferreira", number = 28, female = true},
        new Player{ID = 109, fullName = "Luís Conceição", number = 3, female = false},
        new Player{ID = 110, fullName = "Maria Fonseca", number = 13, female = true},
        new Player{ID = 111, fullName = "Matthew Tupling", number = 1, female = false},
        new Player{ID = 112, fullName = "Pedro", number = 31, female = false},
        new Player{ID = 113, fullName = "Rafael Pifre", number = 0, female = false},
        new Player{ID = 114, fullName = "Rui Ferreira", number = 12, female = false},
        new Player{ID = 115, fullName = "Sara Costa", number = 8, female = true},
        new Player{ID = 116, fullName = "Vanessa Eterno", number = 99, female = true}}
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