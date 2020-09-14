class Message {
    constructor(username, text, when) {
        this.userName = username;
        this.text = text;
        this.when = when;
    }
}

// userName is declared in razor page.
const username = userName;
const textInput = document.getElementById('messageText');
//const whenInput = document.getElementById('when');
const chat = document.getElementById('chat');
const messagesQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
    var currentdate = new Date();
    /*when.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });*/
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;
    
    let when = new Date();
    let message = new Message(username, text);
    sendMessageToHub(message);
}

function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;

    let container3 = document.createElement('div');
    container3.className = "row";

    let container2 = document.createElement('div');
    container2.className = isCurrentUserMessage ? "col-md-6 offset-md-6" : "col-md-6";

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "container darker bg-primary" : "container bg-light";

    let sender = document.createElement('p');
    sender.className = isCurrentUserMessage ? "text-right text-white sender" : "text-left sender";
    //sender.className = "sender";
    sender.innerHTML = message.userName;
    let text = document.createElement('p');
    text.className = isCurrentUserMessage ? "text-right text-white" : "text-left";
    text.innerHTML = message.text;

    let when = document.createElement('span');
    when.className = isCurrentUserMessage ? "time-right text-light" : "time-right";
    var currentdate = new Date();
    when.innerHTML = 
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    container3.appendChild(container2);
    container2.appendChild(container);
    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(when);
    chat.appendChild(container3);
}
