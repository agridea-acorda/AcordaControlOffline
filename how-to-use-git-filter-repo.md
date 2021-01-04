# Petit manuel pour git-filter-repo

## A quoi ça sert ?

`git-filter-repo` est une extension pour git permettant de réécrire l'historique d'une repo git. Elle n'est pas incluse par défaut avec git, il faut l'installer séparément. Elle est utile pour supprimer des informations sensibles qui ont été commitées par le passé dans une repo (mots de passe, documents condifentiels, ...). La ré-écriture de l'historique est une opération ***destructive*** et ***irréversible***: la repo est 'trafiquée' pour modifier l'historique des versions, ce n'est pas une opération git standard. Pour cette raison, l'auteur recommande de travailler sur une repo locale fraichement clonée et de backuper au préalable la repo distante.
Prendre le temps de lire au préalable la [page github](https://github.com/newren/git-filter-repo) et la [page de manuel](https://www.mankier.com/1/git-filter-repo) de l'outil.

## Installation

- Installer ou mettre à jour `python`. Avec python 3.7+ ça fonctionne. Vérifier que le chemin d'installation de python3.exe est dans le $PATH. `git-filter-repo` est un script python.
- Installer [scoop](https://scoop.sh/). C'est la méthode recommendée sous windows. Dans une console powershell: `Invoke-Expression (New-Object System.Net.WebClient).DownloadString('https://get.scoop.sh')`. Si erreur essayer de changer l'execution policy: `Set-ExecutionPolicy RemoteSigned -scope CurrentUser`
- Utiliser scoop pour [installer](https://github.com/newren/git-filter-repo/blob/main/INSTALL.md) git-filter-repo: `scoop install git-filter-repo`
- Si l'executable python est python.exe au lieu de python3.exe (voir [doc](https://github.com/newren/git-filter-repo/blob/main/INSTALL.md)) alors il faut changer la première ligne du script `git-filter-repo` comme suit: `#!/usr/bin/env python` (enlever le '3' de 'python3').

## Usage

- [Analyser](https://www.mankier.com/1/git-filter-repo#--analyze) la repo: `--analyse`.
- [Rechercher](https://git-scm.com/book/en/v2/Git-Tools-Searching) dans la repo des éléments suspicieux à supprimer tels que identifiants, mots de passe, ou fichier confidentiels tels qu'extraits de données.
- Pour acordacontrol j'ai utilisé deux opérations: supprimer un répertoire entier contenant des extractions de données (plus exactement garder tout sauf un/des répertoires, voir [Path based filtering](https://www.mankier.com/1/git-filter-repo#Examples-Path_based_filtering)), et
- Remplacer du texte, par exemple des login/mots de passe ,voir [content based filtering](https://www.mankier.com/1/git-filter-repo#Examples-Content_based_filtering). J'ai par exemple remplacé les occurences de `suser` ou `aco_nde` et du mot de passe correspondant par des dummy `LOGIN-REMOVED` ou `PASSWORD-REMOVED`. La technique décrite dans la doc `git filter-repo --replace-text mes-mots-a-remplacer.txt` fonctionne bien.