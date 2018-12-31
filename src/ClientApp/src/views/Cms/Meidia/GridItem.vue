<template>
  <v-flex lg2 sm3 xs4 class="pa-2" :class="{selected:selected}">
    <v-card flat tile class="grid-item">
      <v-responsive class="link" @click="$emit('click')" @dblclick="handleDbClick">
        <v-icon size="135" class="mx-auto" color="indigo" v-if="item.IsFolder">folder</v-icon>
        <v-img
          :lazy-src="imgSrc"
          :src="imgSrc"
          alt
          v-else-if="isImage"
          aspect-ratio="1"
          class="grey lighten-2"
        >
          <v-layout slot="placeholder" fill-height align-center justify-center ma-0>
            <v-progress-circular indeterminate color="grey lighten-5"></v-progress-circular>
          </v-layout>
        </v-img>
        <v-icon class="mx-auto" size="135" v-else>insert_drive_file</v-icon>
      </v-responsive>
      <v-divider></v-divider>
      <v-card-title class="grid-item" @dblclick="reName">{{item.Name}}</v-card-title>
    </v-card>
  </v-flex>
</template>

<script>
import { showDialog, alert } from "@/utils";
import Prompt from "@/components/Message/Prompt.vue";
import rules from "@/rules";
import Vue from "vue";
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
      if (this.item.Resource)
        return this.item.Resource.ContentType.startsWith("image/");
    },
    imgSrc() {
      if (this.isImage) {
        return `/api/resource/get/${this.item.Resource_Id}`;
      }
    }
  },
  methods: {
    async reName() {
      let { name } = await showDialog(Prompt, {
        title: "名称",
        maxWidth: "500px",
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
      if (name == this.item.Name) return;
      let beforeName = this.item.Name;
      this.item.Name = name;

      let postData = { ...this.item };
      delete postData.Foreign;
      delete postData.Create_User;
      delete postData.Modify_User;

      try {
        await this.$http.put("/api/Meidia/put", postData);
        alert.success("更新成功!");
      } catch (error) {
        this.item.Name = beforeName;
      }
    },
    handleDbClick() {
      this.$emit("dblclick");
      if (this.isImage) {
        let src = this.imgSrc;
        showDialog(
          Vue.extend({
            props: {
              success: Function
            },
            render(h) {
              return h("v-img", {
                props: {
                  "lazy-src": src,
                  src: src,
                  "max-height": "80%",
                  "max-width": "80%"
                },
                on: {
                  click: () => {
                    this.success();
                  }
                }
              });
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
}
.link {
  cursor: default;
}
.selected {
  background-color: #cce8ff;
}
.img {
  max-width: 150px;
  max-height: 150px;
}
</style>
