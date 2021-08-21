import * as signalR from "@microsoft/signalr";

import { sleep } from "../utils"

import store from '../store'

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub/message")
    .configureLogging(signalR.LogLevel.Information)
    .build()

connection.onclose(function () {
    window.console.log(`signalR断开连接`);
    onError();
})

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
        window.console.log('signalR开始连接');
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
    window.console.error(err) 
    window.console.error(err.message) 
    window.console.error('5秒后重新连接') 
    await sleep(5000)
    if (store.state.currUser && store.state.currUser.Id) { 
        start()
    }
}

async function onConnectioned() {
    window.console.log('signalR连接成功');
}

connection.on("Notify", function () {
    window.console.log(...arguments)
})

connection.on("Confirm", function () {
    window.console.log(...arguments)
})

connection.on("Choose", function () {
    window.console.log(...arguments)
})

connection.on('inited', function () {
    window.console.log(...arguments)
})

