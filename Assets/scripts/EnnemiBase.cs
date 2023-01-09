using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiBase : Personnages
{
    // Start is called before the first frame update

    //mettre les attribut : e qui permet de caractériser les objets

    private String name = "ali";
    private double life = 20;
    private double attack = 1;


    //


    //constructeur
    public EnnemiBase()
    {

    }


    public EnnemiBase(String name, double attack, double life)
    {
        //cvd ce qu'on va entrer en paramètre dans ce constructeur correspondra au nouveau name , nouveau life et au nive attack de ce jnouveau joueur
        this.name = name;
        this.attack = attack;
        this.life = life;

    }

    //fonction pour modifier le nom et pour le recuperer:
    //getteur: permet de recuperer le nom et le setteur permet de le modifier 

    public String Getname()
    {
        return name;
    }


    // on cree une methode qui va permettre d'enlever les point de vie d'un joueur


    public override void Setname(string name)
    {
        this.name = name;
    }

    public override double Getlife()
    {
        return life;
    }

    public override void Setlife(double life)
    {
        this.life = life;
    }

    public override double Getattack()
    {
        return attack;
    }

    public override void Setattack(double attack)
    {
        this.attack = attack;
    }

    public override void damageattack(double damage)
    {
        this.life -= damage;
    }



    // Start is called before the first frame update
}
