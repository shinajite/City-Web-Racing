# CityWebRacing?
## About This
henohenomoheji

## Play Game
### 1. Download the release file
url<br>
![addRepoImage](../readmeImages/2023-11-20 155939.png)

### 2. Open the downloaded file
find the downloaded file in Explorer or Finder and open it.
note: if you can't open file, please try right click→open

Enjoy!

## Development
### 1. Clone or download this repository
https://docs.github.com/ja/get-started/getting-started-with-git/about-remote-repositories#cloning-with-https-urls<br>
you can use GitHub Desktop, CMD, SSH, CLI, or Subversion.
ofcourse, you can use this as download.
![addRepoImage](./readmeImages/2023-11-20%20163234.png)

### 2. Open in Unity
first, you have to install unity hub.<br>
https://unity.com/ja/download<br>

then, install Unity version 2021.3.28<br>
https://unity.com/releases/editor/whats-new/2021.3.28#release-notes

finaly, add your repository on unity hub.<br>
![addRepoImage](./readmeImages/2023-11-20%20155939.png)<br>

you can open this project when you click project.<br>

### 3. Set secrets file
please move to Assets/Scripts and create C# file name Secrets.cs.<br>
note: right crick→create→c#script input name as Secrets

and replace Secrets.cs as this code.
```cs
public static class Secrets
{
    public const string API_KEY = "YOUR_API_KEY";
}
```
note: please replace "YOUR_API_KEY".

### 2. Open local scean
please move to Assets/Scenes and open "localScene".<br>
![addRepoImage](./readmeImages/2023-11-20%20163029.png)<br>
you can run and play game

## Multi Play
### 1. Link your unity relay project
https://unity.com/ja/products/relay<br>
https://docs.unity.com/ugs/en-us/manual/relay/manual/get-started<br>
note: you must not install packages. only for link.<br>
note: we are not responsible for any charges incurred as a result of this

### 2. Open multi play scene
please move to Assets/Scenes and open scene named "multiRaceWi".
![openMultiImage](./readmeImages/2023-11-20%20160433.png)

### 3. Host and join game
first, host player have to run projects and click host.<br>
and, share join code left up on view.<br>
![openMultiImage](./readmeImages/2023-11-20%20163503.png)<br>
join playeres run projects and input shared join code in input and click client button.
![openMultiImage](./readmeImages/2023-11-20%20163911.png)<br>

Enjoy!