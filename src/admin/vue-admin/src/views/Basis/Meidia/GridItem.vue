<template>
  <v-flex xs12 sm3 md3 lg2 xl1 :class="{selected:selected}">
    <v-card height="176" @click="$emit('click')">
      <v-card-text @dblclick="$emit('dblclick')" style="padding:0px;">
        <v-icon size="135" class="mx-auto" color="indigo" v-if="item.IsFolder">folder</v-icon>
        <img
          :src="imgSrc"
          v-else-if="isImage"
          class="card-image"
          style="max-width:100%;max-height:135px;"
          @dblclick.stop="handleDbClick"
        />
        <v-icon size="135" class="mx-auto" color="indigo" v-else>insert_drive_file</v-icon>
      </v-card-text>
      <v-divider></v-divider>
      <v-card-actions class="grid-item" @dblclick="reName">
        <nobr>{{item.Name}}</nobr>
      </v-card-actions>
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
      return (
        this.item.Resource &&
        this.item.Resource.ContentType.startsWith("image/")
      );
    },
    imgSrc() {
      if (this.isImage) {
        return `/api/resource/get/${this.item.Resource_Id}`;
      } else {
        return null;
      }
    }
  },
  methods: {
    async reName() {
      let dialog = showDialog(Prompt, {
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
      dialog.then(({ name }) => {
        if (name == this.item.Name) return;
        let beforeName = this.item.Name;
        this.item.Name = name;

        let postData = { ...this.item };
        delete postData.Foreign;
        delete postData.Create_User;
        delete postData.Modify_User;

        this.$http
          .put("/api/Meidia/put", postData)
          .then(() => {
            alert.success("更新成功!");
          })
          .catch(() => {
            this.item.Name = beforeName;
          });
      });
    },
    handleDbClick() {
      // this.$emit("dblclick");
      if (this.isImage) {
        let src = this.imgSrc;
        showDialog(
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
