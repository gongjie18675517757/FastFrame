import * as signalR from "@aspnet/signalr";
import {sleep} from '@/utils.js'

const connection = new signalR.HubConnectionBuilder()
  .withUrl("/hub/chat")
  .build()

// connection.start().catch(function (err) {
//   return console.error(err.toString());
// });

connection.onclose(onError)

const start = async () => {
  try {
    console.log('开始连接');
    await connection.start()
    onConnectioned()
  } catch (error) {
    console.log('连接失败');
    await onError(error)
  }
}


const onError = async (err) => {
  window.console.log(err)
  await sleep(5000)
  await start()
}

async function onConnectioned() {
  while (true) {   
    await connection.invoke('SendMessage', 'xx', 'xxxxxx')
    await sleep(10000)
  }
}

connection.on("ReceiveMessage", (username, message) => {
  window.console.log(username, message)
})

start()