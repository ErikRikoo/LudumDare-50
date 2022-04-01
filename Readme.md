# Ludum Dare 50

**Remrque:** Il faut penser à ne pas modifier ensemble la scène car ce sera impossible à merge après coup. Vous pouvez donc utiliser un maximum les prefabs. Vous trouverez un prefab par personne avec votre nom dans la scène MainScene.

## Installation
Pour installer le projet sur votre ordinateur, il vous faut tout d'abord git. Ensuite, éxécutez ces commandes:
```
git clone https://github.com/ErikRikoo/LudumDare-50.git
git flow init
```
Pour la dernière commande, il va vous demander quelles sont les branches à utiliser. Juste appuyez sur Entrée à plusieurs reprises.

### Utilisation de Git Flow
Nous allons utiliser git flow afin de se simplifier la ie avec Git. Après avoir fais `git flow init`, vous devrez être sur la branche *develop*. Vous pouvez vérifier facilement en faisant `git branch`, la branche courante sera verte. La branche *develop* sert à contenir toutes les modifications en cours et la branche *master* servira pour les release et qui devra contenir que des versions fonctionnelles.

Vous pouvez si vous le désirez utiliser la notion de feature qui vous met sur une autre branche basée sur develop. Vous avez une commande pou créer une feature:
```
git flow feature start nom_feature
```

Et une pour la finir, ce qui va la merge dans develop et la supprimer:
```
git flow feature finish
```

## Contenu du projet initial
Le projet contient déjà les dossiers nécessaires au bon déroulement du projet. Vous trouverez aussi la scène MainScene qui contient un prefab par personne.

Certains paquets se trouvent déjà dans le projet:
- [Atom](https://github.com/unity-atoms/unity-atoms) qui permet de facilement stocker des variables et des événements dans le projet.
- [Editor Utilities](https://github.com/ErikRikoo/com.rikoo.editor-utilities) qui est un paquet maison contenant de nombreuses ressources tels que des attributs (AbstractReference permet de facilement afficher des classes abstraites dans l'Inspector).
- [Triggerable](https://github.com/ErikRikoo/com.rikoo.triggerable) qui permet de facilement mettre en place des systèmes trigger -> action.
- [Animation Package](https://docs.unity3d.com/Packages/com.unity.animation.rigging@0.2/manual/index.html) qui aide à faire des animations procédurales.
- [Timeline](https://docs.unity3d.com/Packages/com.unity.timeline@1.2/manual/index.html) pour les cinématiques.
- [ProBuilder](https://docs.unity3d.com/Packages/com.unity.probuilder@4.0/manual/index.html) pour faire du blocking.