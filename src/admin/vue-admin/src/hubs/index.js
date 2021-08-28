import * as signalR from "@microsoft/signalr";

import { sleep } from "../utils"

import store from '../store'
import message from "../components/Message";

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
    if (err) {
        window.console.error(err)
    }
    await sleep(5000)
    if (store.state.currUser && store.state.currUser.Id) {
        start()
    }
}

async function onConnectioned() {
    window.console.log('signalR连接成功');
}

connection.on("client.notify", function ([msg]) {
    msg = JSON.parse(msg);

    store.state.newNotifys.push(msg);
    store.state.notifys.push(msg);
})

connection.on("client.confirm", async function ([msg]) {
    msg = JSON.parse(msg);
    window.console.log(msg);
    let result = false;
    try {
        await message.confirm({
            title: msg.Title,
            content: msg.Content,
            timeoute: msg.Timeout
        })
        result = true;
    } finally {
        connection.invoke('ClientResponse', JSON.stringify({
            MsgType: 'client.confirm',
            MsgContent: JSON.stringify({
                Id: msg.Id,
                Result: result
            })
        }))
    }

})

connection.on("client.choose", function () {
    window.console.log(...arguments)
})

connection.on("client.onConnected", function () {
    window.console.log(...arguments)
})



