var connection = new signalR.HubConnectionBuilder().withUrl('/IdHome/Index').build();

connection.on('receivedMessage', addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToHub(message) {
    connection.invoke('SendMessage', message)
}