import store from "../../store";
import httpClient from '../../httpClient'
import message from "../Message";
import { getEnumValues } from "../../generate";
import rules from "../../rules";

class makeButtonsInput { }
/**
 * 表单模型数据
 */
makeButtonsInput.prototype.selection = [];

/**
 * 模式 form/list/row
 */
makeButtonsInput.prototype.mode = '';

/**
 * 按钮属性
 */
makeButtonsInput.prototype.btnAttrs = new Object();

/**
 * 审核模块的名称
 */
makeButtonsInput.prototype.moduleName = '';

/**
 * 是否在编辑中
 */
makeButtonsInput.prototype.editing = false;

/**
 * 生成按钮模式
 */
export const makeButtonsInputMode = {
    /**
     * 表单
     */
    FORM: 'FORM',

    /**
     * 列表工具条
     */
    LIST: 'LIST',

    /**
     * 单元格
     */
    CELL: 'CELL'
}

/**
 * 审核操作参数
 */
function handleFlowOperateInput() { }

/**
 * 可执行的操作
 */
handleFlowOperateInput.prototype.actions = [];

/**
 * 标题
 */
handleFlowOperateInput.prototype.title = [];

/**
 * 审核操作
 * @param {handleFlowOperateInput} params 
 */
async function handleFlowOperate(params) {
    await httpClient.get('/')
    if (params.actions.length == 0)
        throw new Error('actions传参不正确!');

    /**
     * 审核动作
     */
    let kvs = (await getEnumValues('FlowStep', 'Action')).filter(v => params.actions.includes(v.Key))

    message.prompt({
        width:'600px',
        title: `${params.title}`,
        submitFunc:model=>{
            console.log(model);
        },
        model: {
            ActionEnum: params.actions.find(v => v),
            Desc: null,
            NextCheckerIds: [],
            Items: null
        },
        options: [
            {
                Name: 'ActionEnum',
                Description: `${params.title}选项`,
                EnumValues: kvs,
                Readonly: kvs.length <=1,
                IsRequired: true
            },
            {
                Name: 'Desc',
                Description: `${params.title}意见`,
                Type: 'String',
                Length: 500,
            },
        ],
        rules: {
            ActionEnum: [
                rules.required(`${params.title}选项`)
            ],
            Desc: [
                rules.stringLength(`${params.title}意见`, 0, 500)
            ]
        },
        ...params
    })
}

/**
 * 提交
 * @param {*} params 
 */
async function submit(params) {
    event.stopPropagation();
    await handleFlowOperate({ 
        ...params,
        title:'提交',
        actions: ['submit']
    })
}

/**
 * 审核
 * @param {*} params 
 */
async function check(params) {
    event.stopPropagation();
    await handleFlowOperate({
        ...params,
        title:'审核',
        actions: ['pass', 'ng']
    })
}




/**
 * 生成流程按钮
 * @param {makeButtonsInput} param0  生成参数
 * @returns 
 */
export function makeButtons({ selection, mode, btnAttrs, moduleName, editing }) {
    btnAttrs = btnAttrs || {};
    const modeValues = Object.values(makeButtonsInputMode)
    if (!modeValues.includes(mode)) {
        throw new Error(`mode参数不正确,请传入${modeValues.join('/')}`)
    }

    return [
        {

            functional: true,
            render: (h) => {
                if (editing)
                    return null;

                if (mode != makeButtonsInputMode.LIST && !selection.some(v => v && v.Id))
                    return null;

                /**
                 * 当前登陆的用户
                 */
                const curr_id = store.state.currUser ? store.state.currUser.Id : null;



                /**
                 * 是否可以提交
                 */
                const canSubmit = selection.some(v => v.Create_User_Id == curr_id && ['unsubmitted', 'ng'].includes(v.FlowStatus));

                /**
                 * 是否可以审核
                 */
                const canCheck = canSubmit

                /**
                 * 是否显示提交按钮
                 */
                const visibleSubmit = mode == makeButtonsInputMode.LIST ||
                    (mode == makeButtonsInputMode.FORM && canSubmit) ||
                    (mode == makeButtonsInputMode.CELL && canSubmit);

                /**
                 * 是否显示审核按钮
                 */
                const visibleCheck = mode == makeButtonsInputMode.LIST ||
                    (mode == makeButtonsInputMode.FORM && canSubmit) ||
                    (mode == makeButtonsInputMode.CELL && canSubmit);

                return h('fragments-facatory', null, [
                    /**
                     * 提交
                     */
                    visibleSubmit ?
                        h('v-btn', {
                            attrs: {
                                'x-small': mode == makeButtonsInputMode.CELL,
                                text: mode == makeButtonsInputMode.CELL,
                                outlined: mode == makeButtonsInputMode.FORM,
                                tile: mode == makeButtonsInputMode.FORM,
                                color: 'primary',
                                ...btnAttrs,
                                disabled: !canSubmit
                            },
                            on: {
                                click: () => submit({ moduleName, selection })
                            }
                        }, [
                            [makeButtonsInputMode.LIST, makeButtonsInputMode.FORM].includes(mode) ? h('v-icon',
                                { attrs: { left: true } },
                                selection.length > 1 ? 'mdi-chevron-double-up' : 'mdi-chevron-up'
                            ) : null,
                            h('span', '提交'),
                        ]) :
                        null,

                    /**
                     * 审核
                     */
                    visibleCheck ? h('v-btn', {
                        attrs: {
                            'x-small': mode == makeButtonsInputMode.CELL,
                            text: mode == makeButtonsInputMode.CELL,
                            outlined: mode == makeButtonsInputMode.FORM,
                            tile: mode == makeButtonsInputMode.FORM,
                            color: 'primary',
                            ...btnAttrs,
                            disabled: !canCheck
                        },
                        on: {
                            click: () => check({ moduleName, selection })
                        }
                    }, [
                        [makeButtonsInputMode.LIST, makeButtonsInputMode.FORM].includes(mode) ? h('v-icon',
                            { attrs: { left: true } },
                            selection.length > 1 ? 'mdi-check-all' : 'mdi-check'
                        ) : null,
                        h('span', '审核'),
                    ]) : null,
                ])
                // return selection.length > 0 ? h('a', null, 'test') : null
            }

        }
    ]
}