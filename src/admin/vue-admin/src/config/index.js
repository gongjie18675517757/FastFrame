/**
 * 当前是否开发环境
 */
export const isDev = process.env.NODE_ENV == 'development'

/**
 * 上传路径
 */
export function getDownLoadPath(fileId, fileName) {
    fileName = fileName || ''
    fileName = encodeURI(fileName)
    return `${isDev ? '/api' : ''}/resources/download/${fileId}/${fileName}`
}

/**
 * 下载路径
 */
export function getUploadPath() {
    return `${isDev ? '/api' : ''}/resources/upload` 
}
