var gameOverElement = document.getElementById("gameOverScreen");
var canvas = document.querySelector('canvas');
var playerScoreElement = document.getElementById("playerScore");
var playerSpeedElement = document.getElementById("playerSpeed");
var overlayUiElement = document.getElementById("overlayUiContainer");



function popGameOverScreen()
{
	overlayUiElement.style.display = "none";
	gameOverElement.style.display = "inline";

	playerScoreElement.innerHTML = "Speed : " +speedElement.innerHTML;
	playerSpeedElement.innerHTML = "Score : " + Math.floor(playerScore);

	window.addEventListener("keydown",(event)=>{reloadPage(event)})
}

function reloadPage(event)
{
	
	if (event.key == "r")
	{
		window.location.reload();
	}
}


