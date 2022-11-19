
/**
 * 生成查询
 */
class QueryBuild {
    constructor(arr = []) {
        this._querys = arr
    }
    /**
     * 添加
     * @returns {QueryBuild}
     */
    add() {
        this._querys.push(...arguments)
        return this;
    }

    /**
     * 添加and
     * @returns {QueryBuild}
     */
    add_and() {
        const arr = [...arguments]
        if (!arr.some(v => v.length))
            return;

        this._querys.push({
            "Key": "and",
            "Value": arr
        })
        return this;
    }
    /**
     * 添加or
     * @returns {QueryBuild}
     */
    add_or() {
        const arr = [...arguments]
        if (!arr.some(v => v.length))
            return;

        this._querys.push({
            "Key": "or",
            "Value": arr
        })
        return this;
    }

    /**
     * 生成查询
     * @returns {Array}
     */
    build() {
        let arr = JSON.parse(JSON.stringify(this._querys));
        for (const f of arr) {
            for (let index = 0; index < f.Value.length; index++) {
                const arr = f.Value[index];
                for (const v of arr) {
                    if (Array.isArray(v.value))
                        v.value = v.value.join(',')
                }

                f.Value[index] = arr.filter(x => !!x.value)
            }

            f.Value = f.Value.filter(arr => arr.length)
        }

        arr = arr.filter(v => v.Value.length > 0);

        return arr;
    }
}


export default QueryBuild;


const filters = [
    {
        Name: 'a',
        Compare: '=',
        Value: 'abc'
    },
    {
        ComposeMode: 'and',
        QueryFilters: [
            {
                Name: 'a',
                Compare: '=',
                Value: 'abc'
            },
            {
                Name: 'a',
                Compare: '=',
                Value: 'abc'
            },
            {
                ComposeMode: 'and',
                QueryFilters: [
                    {
                        Name: 'a',
                        Compare: '=',
                        Value: 'abc'
                    },
                    {
                        Name: 'a',
                        Compare: '=',
                        Value: 'abc'
                    },
                ]
            }
        ]
    }
]