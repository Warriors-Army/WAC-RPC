# WAC-RPC
Le Rich Presence de la Warriors Army

Vous pouvez le modifier en remplaçant cette partie du [Program.cs](WAC/WAC/Program.cs) :
```C#
client.SetPresence(new RichPresence()
{
    Details = "Texte du haut",
    State = "Texte du bas",
    Assets = new Assets
    {
        LargeImageKey = "Image large",
        LargeImageText = "Texte de l'image large",
        SmallImageKey = "Image petite",
        SmallImageText = "Texte de l'image petite"
    },
    Buttons = new Button[] {
        new Button
        {
            Label = "Texte du bouton",
            Url = "Lien du bouton"
        },
        new Button
        {
            Label = "Texte du 2e bouton",
            Url = "Lien du 2e bouton"
        }
    }
});
```
Noubliez pas de mettre l'ID de votre [application Discord](https://discord.com/developers/applications) ici :
```C#
DiscordRpcClient client = new DiscordRpcClient("ID de l'appli");
```

Les noms des images sont ceux des images que vous avez upload sur votre appli dans l'onglet "Art Assets" de l'onglet "Rich Presence"
![image](https://user-images.githubusercontent.com/73444916/122687537-3dedaf80-d217-11eb-93c1-f17cde46e2b7.png)

Pour lancer l'appli vous avez besoin de ce qui se trouve dans le dossier [publish](WAC/WAC/publish) et lancer le [setup.exe](WAC/WAC/publish/setup.exe)
