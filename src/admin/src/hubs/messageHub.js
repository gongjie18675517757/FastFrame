import * as signalR from "@aspnet/signalr";
import {
  alert,
  sleep,
  eventBus
} from "@/utils"

import store from '@/store'

const connection = new signalR.HubConnectionBuilder()
  .withUrl("/hub/message")
  .build()

connection.onclose(onError)


/**
 * 连接
 */
export async function start() {
  console.log(11);

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
  if (store.state.currUser && store.state.currUser.Id) {
    window.console.err(err)
    await sleep(5000)
    await start()
  }
}

async function onConnectioned() {
  // while (store.state.currUser && store.state.currUser.Id && connection.state == 1) {
  //   await connection.invoke('SendMessage', 'xx', 'xxxxxx')
  //   await sleep(10000)
  // }
}

connection.on("FriendMsg", (msg) => {
  eventBus.$emit('FriendMsg', msg)
  store.commit({
    type: 'addFriendMsg',
    FriendMsg: msg
  })
})

connection.on("Notify", (msg) => {
  alert.success(msg.content)
})

 

connection.on('ReceiveMessage', (msg) => {
  if (typeof msg == 'string')
    msg = JSON.parse(msg)
  let {
    Category,
    Content,
    TypeName
  } = msg;

  if (Category) {
    eventBus.$emit(`${TypeName}_${Category}`, Content)
  }

  // eventBus.$emit('data_add', msg)
})

eventBus.$on('init', () => {
  start()
  console.log(111);
})