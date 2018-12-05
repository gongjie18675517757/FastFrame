import * as signalR from "@aspnet/signalr";
import {
  alert
} from "@/utils"
import {
  sleep
} from '@/utils.js'
import {
  eventBus
} from '@/utils'
import store from '@/store'

const connection = new signalR.HubConnectionBuilder()
  .withUrl("/hub/message")
  .build()

// connection.start().catch(function (err) {
//   return console.error(err.toString());
// });

// connection.onclose(onError)

/**
 * 连接
 */
export async function start() {
  try {
    connection.stop();
  } catch (err) {
    window.console.err(err)
  }
  try {
    window.console.log('开始连接');
    await connection.start()
    onConnectioned()
  } catch (error) {
    window.console.error(error);
    onError(error)
  }
}
/**
 * 停止
 */
export async function stop() {
  await connection.stop()
}


const onError = async (err) => {
  window.console.err(err)
  await sleep(5000)
  await start()
}

async function onConnectioned() {
  // while (true) {
  //   await connection.invoke('SendMessage', 'xx', 'xxxxxx')
  //   await sleep(10000)
  // }
}

connection.on("FriendMsg", (msg) => {
  eventBus.$emit('FriendMsg', msg)
  store.commit({
    type: 'addFriendMsg',
    FriendMsg:msg
  })
})

connection.on("Notify", (msg) => {
  alert.success(msg.content)
})


eventBus.$on('init', start)