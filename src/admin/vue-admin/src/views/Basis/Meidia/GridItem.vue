<template>
  <v-card @dblclick="$emit('dblclick')" :dark="selected">
    <v-img
      :src="src"
      :lazy-src="src"
      class="white--text align-end"
      gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)"
      height="150px"
      max-width="100%"
      @click="$emit('click')"
      :style="{cursor:item.IsFolder || 1 ?'pointer':''}"
    >
      <v-card-title v-text="item.Name"></v-card-title>
    </v-img>
    <!-- <v-icon size="200" class="mx-auto" color="indigo" v-else-if="item.IsFolder">folder</v-icon>
    <v-icon size="200" class="mx-auto" color="indigo" v-else>insert_drive_file</v-icon>-->
    <!-- <v-card-text @dblclick="$emit('dblclick')" style="padding:0px;">
      <v-icon size="135" class="mx-auto" color="indigo" v-if="item.IsFolder">folder</v-icon>
      <img
        :src="imgSrc"
        v-else-if="isImage"
        class="card-image"
        style="max-width:100%;max-height:135px;"
        @dblclick.stop="handleDbClick"
      />
      <v-icon size="135" class="mx-auto" color="indigo" v-else>insert_drive_file</v-icon>
    </v-card-text>-->
    <v-divider></v-divider>
    <v-card-actions class="grid-item" @dblclick="reName">
      <v-spacer></v-spacer>
      <v-btn icon v-if="!item.IsFolder" title="下载" @click="download">
        <v-icon>mdi-download</v-icon>
      </v-btn>

      <v-btn icon title="重命名" @click="reName">
        <v-icon>mdi-file-edit</v-icon>
      </v-btn>

      <v-btn icon title="删除" @click="remove">
        <v-icon>mdi-delete</v-icon>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import rules from "../../../rules";
import Vue from "vue";
import folder from "../../../assets/folder.svg";
import file from "../../../assets/file.svg";
import xlsx from "../../../assets/xlsx.svg";
import pptx from "../../../assets/pptx.svg";
import js from "../../../assets/js.svg";
import txt from "../../../assets/TXT.svg";
export default {
  props: {
    item: {
      type: Object,
      default: function() {
        return {};
      }
    },
    selected: Boolean
  },
  computed: {
    isImage() {
      return (
        this.item.ContentType && this.item.ContentType.startsWith("image/")
      );
    },
    src() {
      let contentType = this.item.ContentType;
      if (this.isImage) {
        return `/api/resource/get/${this.item.Resource_Id || ""}`;
      } else if (this.item.IsFolder) {
        return folder;
      } else if (!this.item.ContentType) {
        return file;
      } else {
        switch (contentType) {
          case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
            return xlsx;

          case "application/vnd.ms-powerpoint":
            return pptx;

          case "text/javascript":
            return js;

          case "text/plain":
            return txt;
          default:
            return file;
        }
      }
    }
  },
  methods: {
    remove() {
      this.$emit("remove", this.item);
    },
    async reName() {
      let dialog = this.$message.prompt({
        title: "名称",
        width: "500px",
        model: {
          name: this.item.Name
        },
        options: [
          {
            Name: "name",
            Type: "String",
            Description: "名称",
            rules: [rules.required("文件夹名称")]
          }
        ]
      });
      dialog.then(({ name }) => {
        if (name == this.item.Name) return;
        let beforeName = this.item.Name;
        this.item.Name = name;

        this.$http
          .put(`/api/Meidia/Put/${this.item.Id}?name=${name}`)
          .catch(() => {
            this.item.Name = beforeName;
          });
      });
    },
    download(){
      window.open(`/api/resource/get/${this.item.Resource_Id}/${this.item.Name}`)
    },
    handleDbClick() {
      // this.$emit("dblclick");
      if (this.isImage) {
        let src = this.imgSrc;
        this.$message.dialog(
          Vue.extend({
            props: {
              success: Function
            },
            render(h) {
              let img = h("v-img", {
                props: {
                  "lazy-src": src,
                  src: src
                },
                on: {
                  click: () => {
                    this.success();
                  }
                }
              });

              return h(
                "v-layout",
                {
                  props: {
                    "grid-list-xl": true,
                    fluid: true,
                    app: true,
                    "align-center": true,
                    "justify-center": true
                  }
                },
                [
                  h(
                    "v-flex",
                    {
                      xs12: true
                    },
                    [img]
                  )
                ]
              );
            }
          })
        );
      }
    }
  }
};
</script>

<style scoped>
.grid-item {
  border-width: 1px;
  border-style: solid;
  border-color: #eee;
  text-align: center;
  white-space: nowrap;
  text-overflow: ellipsis;
}
.link {
  cursor: default;
}
.selected {
  background-color: #cce8ff;
}
.card-image {
  max-width: 100%;
  max-height: 135px;
  display: block;
  margin: 0 auto;
}
</style>
