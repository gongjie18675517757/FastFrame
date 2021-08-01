import folder from "../assets/folder.svg";
import file from "../assets/file.svg";
import xlsx from "../assets/xlsx.svg";
import pptx from "../assets/pptx.svg";
import js from "../assets/js.svg";
import txt from "../assets/TXT.svg";
import pdf from "../assets/Pdf.svg";
import { getThumbnailPath } from "../config";
export default {
    folder,
    file,
    xlsx,
    pptx,
    js,
    txt,
    pdf
}



/**
 * 获取图标类型
 * @param {*} param0 
 */
export function getIcon(pars) {

    let { ContentType, Resource_Id, isImage, IsFolder, Id, Name } = pars
    if (isImage == undefined) {
        isImage = ContentType && ContentType.startsWith("image/")
    }

    if (isImage) {
        return getThumbnailPath(Resource_Id || Id, Name)
    } else if (IsFolder) {
        return folder;
    } else if (!ContentType) {
        return file;
    } else {
        switch (ContentType) {
            case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                return xlsx;
            case "application/vnd.ms-excel":
                return xlsx;
            case "application/vnd.ms-powerpoint":
                return pptx;

            case "text/javascript":
                return js;

            case "text/plain":
                return txt;

            case "application/pdf":
                return pdf;

            default:
                return file;
        }
    }
}