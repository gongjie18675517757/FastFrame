<template>
  <v-card tile :loading="loading">
    <v-toolbar dense flat color="transparent" height="30px">
      <v-toolbar-title>{{ title }}</v-toolbar-title>
      <v-btn small text color="primary" v-if="canEdit" @click="upload"> 上传 </v-btn>
      <v-spacer></v-spacer>
      <v-menu offset-y>
        <template v-slot:activator="{ on }">
          <v-btn v-on="on" title="设置" small icon>
            <v-icon>more_vert</v-icon>
          </v-btn>
        </template>
        <v-list dense>
          <v-list-item @click="view = 'grid'">
            <v-list-item-action>
              <v-icon>view_headline</v-icon>
            </v-list-item-action>
            <v-list-item-content>详细模式</v-list-item-content>
          </v-list-item>
          <v-list-item @click="view = 'list'">
            <v-list-item-action>
              <v-icon>view_list</v-icon>
            </v-list-item-action>
            <v-list-item-content>列表模式</v-list-item-content>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-toolbar>
    <v-card-text>
      <v-container fluid v-if="rows.length">
        <v-row v-if="view === 'grid'">
          <v-col
            v-for="item in rows"
            :key="item.Id"
            cols="12"
            sm="4"
            md="3"
            lg="2"
          >
            <GridItem
              :item="item"
              :selected="currItem == item"
              :mode="view"
              :icon="getThumbnailPath(item)"
              @click="handleClick(item)"
              @remove="handleRemove"
              @download="handleDownload"
              @preview="handlePreview"
              :readonly="!canEdit"
            >
              <!-- <template v-slot:action="item"></template> -->
            </GridItem>
          </v-col>
        </v-row>

        <v-layout column v-else>
          <v-list dense class="transparent" width="800px">
            <v-list-item-group v-model="currItem">
              <ListItem
                v-for="item in rows"
                :key="item.Id"
                :item="item"
                :mode="view"
                :selected="currItem == item"
                :icon="getThumbnailPath(item)"
                @click="handleClick(item)"
                @remove="handleRemove"
                @download="handleDownload"
                @preview="handlePreview"
                :readonly="!canEdit"
              />
            </v-list-item-group>
          </v-list>
        </v-layout>
      </v-container>
      <v-container fluid v-else style="height: 100%">
        <v-row align="center" justify="center" style="height: 100%">
          <a @click="upload" style="padding-left: 15px">上传图片</a>
        </v-row>
      </v-container>
    </v-card-text>
  </v-card>
</template>

<script>
import {  getDownLoadPath, getThumbnailPath } from "../../config";
import { upload } from "../../utils";
import GridItem from "./GridItem.vue";
import ListItem from "./ListItem.vue";

export default {
  components: {
    GridItem,
    ListItem,
  },
  props: {
    title: String,
    value: Array,
    canEdit: Boolean,
    fileKey: String,
  },
  data() {
    return {
      view: "grid",
      currItem: null,
      loading: false,
    };
  },
  computed: {
    rows() {
      return this.value.filter((v) => !this.fileKey || v.Key == this.fileKey);
    },
  },
  methods: {
    getThumbnailPath({ Id, Name }) {
      return getThumbnailPath(Id, Name);
    },
    handleClick(item) {
      this.currItem = item;
    },
    handleDownload({ Id, Name }) {
      window.open(getDownLoadPath(Id, Name));
    },
    handleRemove(item) {
      this.$message
        .confirm({
          title: "提示",
          content: "确认要删除吗?",
        })
        .then(() => {
          let index = this.value.findIndex((v) => v == item);
          this.value.splice(index, 1);
        });
    },
    handlePreview({ Id, Name }) {
      let src = getDownLoadPath(Id, Name);
      this.$message.dialog({
        render(h) {
          let img = h("v-img", {
            props: {
              "lazy-src": src,
              src: src,
              "max-height": "80vh",
            },
            on: {
              click: () => {
                this.$emit("success");
              },
            },
          });

          return h(
            "v-layout",
            {
              props: {
                "grid-list-xl": true,
                fluid: true,
                app: true,
                "align-center": true,
                "justify-center": true,
              },
            },
            [
              h(
                "v-flex",
                {
                  xs12: true,
                },
                [img]
              ),
            ]
          );
        },
      });
    },
    onProgress(v) {
      this.loading = true;

      if (v >= 100) {
        this.loading = false;
      }
    },
    verifyFile(arr) {
      if ([...arr].some((x) => !x.type.startsWith("image/"))) {
        this.$message.toast.error("只允许上传图片类型!");
        return false;
      }

      return true;
    },
    upload() {
      upload({
        accept: this.accept,
        onProgress: this.onProgress,
        verifyFileFunc: this.verifyFile,
      }).then(([file]) => {
        this.value.push({
          Id: file.Id,
          ContentType: file.ContentType,
          Key: this.fileKey,
          Name: file.Name,
          Size: file.Size,
          UploaderName: this.$store.state.currUser.Name,
          UploadTime: file.UploadTime,
        });
        this.$emit("input", this.value);
        this.$emit("change", this.value);
      });
    },
  },
};
</script>

<style>
</style>