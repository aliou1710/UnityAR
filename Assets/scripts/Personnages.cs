using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Personnages 
{
   
        //mettre les attribut : e qui permet de caractériser les objets




    //fonction pour modifier le nom et pour le recuperer:
    //getteur: permet de recuperer le nom et le setteur permet de le modifier 

    

    public abstract void Setname(String name);
    public abstract double Getlife();

    public abstract void Setlife(double life);



    public abstract double Getattack();


    public abstract void Setattack(double attack);

    // on cree une methode qui va permettre d'enlever les point de vie d'un joueur
    public abstract void damageattack(double damage);
      
    


}
