/**
 * 转换查询
 * 去除无效的内容
 * @param {*} arr 
 */
function parseQuery(arr) { 
    for (const r of arr) {
        if (r.QueryFilters) {
            r.QueryFilters = parseQuery(r.QueryFilters)
        }
    }
    const brr = arr.filter(calcQueryFilterEnabled);
    
    
    return brr;
}

export default parseQuery;
/**
 * 验证查询是否有效
 * @param {*} q 
 * @returns 
 */
export function calcQueryFilterEnabled(q) {
    if (q.QueryFilters) {
        return q.QueryFilters.length > 0
    }
    else if (Array.isArray(q.value)) {
        return q.value.length > 0
    } else {
        return !!q.value
    }
}