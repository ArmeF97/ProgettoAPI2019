using System;
using System.Collections.Generic;
using ProgettoApi2019;
public class NomiClass
{
    List<String> lista;
    List<String> usati;

    //inizializza generatore di nomi
    //file di testo: ProgettoAPI2019/bin/debug/netcoreapp2.0/Nomi.txt
    public NomiClass(){
        lista = new List<String>();
        usati = new List<String>();
        System.IO.StreamReader file = new System.IO.StreamReader("Nomi.txt");
        while (!file.EndOfStream)
        {
            lista.Add(file.ReadLine());
        }
	}

    //per addent e delent
    //legge un nome dalla lista e lo aggiunge nella lista dei nomi usati
    public String GeneraNome(){
        Random rand = new Random();
        String nome = lista[rand.Next(lista.Count)];
        usati.Add(nome);
        return nome;
    }

    //per addrel e delrel
    //genera un nome di un entità esistente
    public String GeneraNomeUsato()
    {
        Random rand = new Random();
        if (usati.Count > 0)
        {
            String nome = usati[rand.Next(usati.Count)];
            return nome;
        }
        else
        {
            return NomeCasuale();
        }
    }
    
    //genera un nome di entità che potrebbe già esistere oppure no
    public String NomeCasuale()
    {
        Random rand = new Random();
        String nome = lista[rand.Next(lista.Count)];
        return nome;
    }

    public void RimuoviDaUsati(string nome)
    {
        usati.Remove(nome);
    }
}
