import * as signalR from "@aspnet/signalr";
import {
  sleep
} from '@/utils.js'
import {
  eventBus
} from '@/utils'
const connection = new signalR.HubConnectionBuilder()
  .withUrl("/hub/chat")
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
    //console.log('开始连接');
    await connection.start()
    onConnectioned()
  } catch (error) {
    //console.log('连接失败');
    await onError(error)
  }
}
/**
 * 停止
 */
export async function stop() {
  await connection.stop()
}


const onError = async (err) => {
  window.console.error(err)
  await sleep(5000)
  await start()
}

async function onConnectioned() {
  while (true) {
    await connection.invoke('SendMessage', 'xx', 'xxxxxx')
    await sleep(10000)
  }
}

connection.on("ReceiveMessage", () => {
  eventBus.$emit('receive', )
})


eventBus.$on('init', start)