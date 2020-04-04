import * as signalR from "@microsoft/signalr";
import {
    alert,
    sleep,
    eventBus
} from "../utils"

import store from '../store'

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub/message")
    .configureLogging(signalR.LogLevel.Information)
    .build()

connection.onclose(onError)

/**
 * 连接
 */
export async function start() {
    try {
        connection.stop();
    } catch (err) {
        window.console.error(err)
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
    window.console.log('signalR连接成功');
}



connection.on("Notify", (msg) => {
    alert.success(msg.content)
})

eventBus.$on('init', () => {
    start()
})