﻿using System;
using System.Collections.Generic;
using System.IO;

public class NomiClass
{
    private readonly List<string> lista;
    private readonly List<string> usati;

    //inizializza generatore di nomi
    //file di testo: ProgettoAPI2019/bin/debug/netcoreapp2.0/Nomi.txt
    public NomiClass()
    {
        usati = new List<string>();

        var l = File.ReadAllLines("Nomi.txt");
        lista = new List<string>();
        lista.AddRange(l);
    }

    //per addent e delent
    //legge un nome dalla lista e lo aggiunge nella lista dei nomi usati
    public string GeneraNome()
    {
        Random rand = new Random();
        string nome = lista[rand.Next(lista.Count)];
        usati.Add(nome);
        return nome;
    }

    //per addrel e delrel
    //genera un nome di un entità esistente
    public string GeneraNomeUsato()
    {
        Random rand = new Random();
        if (usati.Count > 0)
        {
            string nome = usati[rand.Next(usati.Count)];
            return nome;
        }
        else
        {
            return NomeCasuale();
        }
    }

    //genera un nome di entità che potrebbe già esistere oppure no
    public string NomeCasuale()
    {
        Random rand = new Random();
        string nome = lista[rand.Next(lista.Count)];
        return nome;
    }

    public void RimuoviDaUsati(string nome)
    {
        usati.Remove(nome);
    }
}