<template>
  <div v-if="list_exambatch" class="list_exam">
    <h1>Danh sách đợt thi</h1>
    <div class="Upperbar">
      <div
        v-if="$store.state.acount.authencation.role == 'admin'"
        class="Upperbar_add"
      >
        <button
          @click="
            this.$router.push({
              name: 'exambatch',
              query: { action: 'create' },
            })
          "
        >
          Tạo đợt thi
        </button>
      </div>
      <div v-if="language_id && language" class="upper_parent">
        <div class="name_exambatch">
          <label>Tên đợt thi</label>
          <input
            v-model="data_fetch2.key"
            type="text"
            class="ip_name_exambatch field"
          />
        </div>
        <div class="name_exambatch">
          <label>Nội dung</label>
          <div class="dropdown_from">
            <div
              @click="status_dropdown2 = !status_dropdown2"
              class="field dropdown_input_from"
            >
              <span class="text">
                {{
                  data_fetch2.name == null ? "Chọn hình thức" : data_fetch2.name
                }}</span
              >
              <font-awesome-icon class="icon" :icon="['fas', 'caret-down']" />
            </div>
            <div
              :class="status_dropdown2 == true ? 'dropdown_menu_show' : null"
              class="dropdown_menu"
            >
              <div
                @click="
                  (data_fetch2.name = item.name),
                    (data_fetch2.id = item.id),
                    (status_dropdown2 = false)
                "
                class="dropdown_item"
                v-for="item in data2"
                :key="item.id"
              >
                {{ item.name }}
              </div>
            </div>
          </div>
        </div>
        <div class="btn_search">
          <button @click="search">Tìm kiếm</button>
        </div>
      </div>
    </div>
    <div class="list_exam_child">
      <!-- filter  -->
      <div v-if="language" class="parent_filter">
        <div class="filer_list_exam">
          <div class="dropdown">
            <div
              @click="status_dropdown = !status_dropdown"
              class="dropdown_input"
            >
              <span class="text">
                {{ !data_fetch.name ? "Chọn ngôn ngữ" : data_fetch.name }}</span
              >
              <font-awesome-icon class="icon" :icon="['fas', 'caret-down']" />
            </div>
            <div
              :class="status_dropdown == true ? 'dropdown_menu_show' : null"
              class="dropdown_menu"
            >
              <div
                @click="
                  (data_fetch.name = item.name),
                    (data_fetch.id = item.id),
                    (status_dropdown = false)
                "
                class="dropdown_item"
                v-for="item in data"
                :key="item.id"
              >
                {{ item.name }}
              </div>
            </div>
          </div>
          <div class="btn_filter_parent">
            <button
              class="btn_filter"
              @click="
                (key_chose = data_fetch.id),
                  (key_chose2 = null),
                  (key_search = null)
              "
            >
              Lọc
            </button>
          </div>
        </div>
        <div>
          <strong>Số lượng {{ list_exambatch.length }}</strong>
        </div>
      </div>
      <!-- end filter -->
      <!-- List exam -->
      <div class="list_exams">
        <table v-if="list_exambatch.length > 0" class="table">
          <tr>
            <th>Id</th>
            <th>Tên đợt thi</th>
            <th>Thuộc kỳ thi</th>
            <th>Nội dung</th>
            <th>Ngôn ngữ</th>
            <th>Trạng thái</th>
            <th>Chức năng</th>
          </tr>
          <tr
            :id="'item' + item.id"
            v-for="item in list_exambatch"
            :key="item.id"
          >
            <td>{{ item.id }}</td>
            <td>{{ item.name }}</td>
            <td>{{ exam_id(item.exam_id).name }}</td>
            <td v-if="content_exame && exam">
              <span
                v-for="itemx in exam_id(item.exam_id).exam_type_id"
                :key="itemx"
                >{{ content_exam_id(itemx).name + "  " }}</span
              >
            </td>
            <td v-if="language">
              {{ language_id(exam_id(item.exam_id).language_id).name }}
            </td>
            <td>{{ item.status == true ? "Hiệu lực" : "Vô hiệu lực" }}</td>
            <td class="setting">
              <button
                @click="
                  this.$router.push({
                    name: 'exambatch',
                    query: { action: 'clipboard-cus', id: item.id },
                  })
                "
              >
                <font-awesome-icon
                  class="icon_clipboad"
                  :icon="['fas', 'clipboard-user']"
                />
              </button>
              <button
                @click.prevent="
                  this.$router.push({
                    name: 'exambatch',
                    query: { action: 'detail-cus', id: item.id },
                  })
                "
              >
                <font-awesome-icon
                  class="icon_detail"
                  :icon="['fas', 'people-group']"
                />
              </button>
              <button
                v-if="$store.state.acount.authencation.role == 'admin'"
                @click="
                  this.$router.push({
                    name: 'exambatch',
                    query: { action: 'edit', id: item.id },
                  })
                "
              >
                <font-awesome-icon
                  class="icon_edit"
                  :icon="['fas', 'pen-fancy']"
                />
              </button>
              <button
                v-if="$store.state.acount.authencation.role == 'admin'"
                @click="deletes(item.id)"
              >
                <font-awesome-icon
                  class="icon_trash"
                  :icon="['fas', 'trash-can']"
                />
              </button>
            </td>
          </tr>
        </table>
        <div class="images" v-else>
          <img src="../Imagess/notfound.png" alt="" />
        </div>
      </div>
      <!-- end list exam -->
      <!-- pagnigation -->
      <div class="group_page">
        <div
          @click="chage(this_page - 1)"
          v-if="this_page > 1 && count_page > 0"
          class="pre"
        >
          <font-awesome-icon :icon="['fas', 'arrow-left']" />
        </div>
        <div
          :class="this_page == item ? 'show_item_page' : null"
          @click="chage(item)"
          v-for="item in count_page"
          :key="item"
          class="item_page"
        >
          {{ item }}
        </div>
        <div
          @click="chage(this_page + 1)"
          v-if="this_page < count_page"
          class="next"
        >
          <font-awesome-icon :icon="['fas', 'arrow-right']" />
        </div>
      </div>
      <!-- end pagnigation -->
    </div>
  </div>
</template>
<script>
import { computed, reactive, ref, watch } from "vue";
import store from "@/store";
import { useToast } from "vue-toastification";
export default {
  name: "HomeView",
  created() {
    const check = setInterval(() => {
      if (store.state.exambatch.exambatch != null) {
        clearInterval(check);
        if (this.$route.query.action) {
          if (this.$route.query.action == "detail") {
            store.state.exambatch.data_post.id = Number(this.$route.query.id);
            store.state.exambatch.data_post.name = store.getters[
              "exambatch/getbyid"
            ](Number(this.$route.query.id))[0].name;
            store.state.exambatch.data_post.exam_id = store.getters[
              "exambatch/getbyid"
            ](Number(this.$route.query.id))[0].exam_id;
            for (var x in store.state.exambatch.data_post.exam_data) {
              store.state.exambatch.data_post.exam_data[x] = store.getters[
                "exambatch/getbyid"
              ](Number(this.$route.query.id))[0].exam_data[x];
            }
          }
        }
      }
    }, 200);
  },
  setup() {
    const toast = useToast();
    store.dispatch("exam/get_all");
    store.dispatch("exambatch/get_all");
    store.dispatch("languages/get_all");
    store.dispatch("content_exame/get_all");
    store.state.exambatch.data_post.id = null;
    const exambatch = computed(() => {
      return store.state.exambatch.exambatch;
    });
    const exam = computed(() => {
      return store.state.exam.exam;
    });
    const exam_id = (value) => {
      return store.getters["exam/getbyid"](value);
    };
    const language = computed(() => {
      return store.state.languages.languages;
    });
    const language_id = (value) => {
      return store.getters["languages/getbyid"](value)[0];
    };
    const content_exame = computed(() => {
      return store.state.content_exame.content_exame;
    });
    const content_exam_id = (value) => {
      return store.getters["content_exame/getbyid"](value)[0];
    };
    const data_fetch = reactive({
      name: null,
      id: null,
    });
    const data_fetch2 = reactive({
      name: null,
      id: null,
      key: null,
    });
    const status_dropdown = ref(false);
    const status_dropdown2 = ref(false);

    const data = computed(() => {
      return store.state.languages.languages;
    });
    const data2 = computed(() => {
      return store.state.content_exame.content_exame;
    });

    const this_page = ref(1);
    const limit_page = ref(6);

    const run_app = watch(this_page, () => {
      let page_start = limit_page.value * (this_page.value - 1);
      let page_end = limit_page.value * this_page.value - 1;
      for (var item in list_exambatch.value) {
        const div = document.getElementById(
          "item" + list_exambatch.value[item].id
        );
        if (item >= page_start && item <= page_end) {
          div.classList.contains("hide") ? div.classList.remove("hide") : null;
          div.classList.add("show");
        } else {
          div.classList.contains("show") ? div.classList.remove("show") : null;
          div.classList.add("hide");
        }
      }
    });

    const count_page = computed(() => {
      if (list_exambatch.value == null) {
        return 0;
      }
      return Math.ceil(list_exambatch.value.length / limit_page.value);
    });

    const chage = (item) => {
      this_page.value = item;
      console.log(this_page.value);
    };

    const deletes = (id) => {
      toast.error("Xóa Thành công");
      store.dispatch("exambatch/delete", id);
    };
    const list_exambatch = computed(() => {
      if (key_chose2.value != null && key_search.value != null) {
        return store.state.exambatch.exambatch.filter(
          (e) =>
            store.getters["exam/getbyid"](e.exam_id)[0].exam_type_id.includes(
              key_chose2.value
            ) && e.name.toUpperCase().includes(key_search.value.toUpperCase())
        );
      }
      if (key_chose.value != null) {
        return store.state.exambatch.exambatch.filter(
          (e) =>
            store.getters["exam/getbyid"](e.exam_id).language_id ==
            key_chose.value
        );
      }
      return store.state.exambatch.exambatch;
    });

    const search = () => {
      key_chose.value = null;
      if (
        data_fetch2.id != null &&
        data_fetch2.key != "" &&
        data_fetch2.key != null
      ) {
        key_chose2.value = data_fetch2.id;
        key_search.value = data_fetch2.key;
      }
    };
    const key_chose = ref(null);
    const key_search = ref(null);
    const key_chose2 = ref(null);
    return {
      data,
      data2,
      data_fetch,
      data_fetch2,
      status_dropdown,
      list_exambatch,
      deletes,
      run_app,
      this_page,
      limit_page,
      count_page,
      chage,
      status_dropdown2,
      key_chose,
      key_search,
      search,
      content_exame,
      content_exam_id,
      exam,
      exam_id,
      exambatch,
      language_id,
      language,
    };
  },
  updated() {
    let page_start = this.limit_page * (this.this_page - 1);
    let page_end = this.limit_page * this.this_page - 1;

    for (var item in this.list_exambatch) {
      const div = document.getElementById(
        "item" + this.list_exambatch[item].id
      );
      if (item >= page_start && item <= page_end) {
        div.classList.contains("hide") ? div.classList.remove("hide") : null;
        div.classList.add("show");
      } else {
        div.classList.contains("show") ? div.classList.remove("show") : null;
        div.classList.add("hide");
      }
    }
  },
};
</script>
<style scoped>
.parent_filter {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.show_item_page {
  background: var(--color_table);
  color: white !important;
}
.hide {
  display: none;
}
.show {
  display: table-row !important;
}
.ip_name_exambatch {
  padding-left: 10px;
}
/* pagenigation */
.group_page {
  position: absolute;
  display: flex;
  bottom: 15px;
  right: 30px;
}
.group_page > div {
  color: var(--color_table);
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: var(--color_table);
}

.item_page:hover {
  background: var(--color_table);
  color: white;
}
.pre,
.next {
  border: 1px solid var(--color_table);
}

/* end pagnagation */
/* list exam  */
table {
  border-collapse: collapse;
  width: 100%;
}
.tale {
  margin-left: auto;
  margin-right: auto;
}

th,
td {
  padding: 8px;
  text-align: center;
  vertical-align: middle !important;
}

tr:nth-child(even) {
  background-color: var(--color_row);
}

th {
  background-color: var(--color_table);
  color: white;
}

.setting > button {
  border-radius: 20px;
  background: white;
  border: none;
  margin-left: 10px;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.icon_trash,
.icon_edit,
.icon_clipboad,
.icon_detail {
  font-size: 20px;
  padding: 5px;
}
.icon_trash {
  color: red;
}
.icon_edit {
  color: var(--color_edit);
}

/* end list exam  */
/* filter list */
.btn_filter_parent {
  display: flex;
  align-items: center;
  justify-content: center;
}
.filer_list_exam {
  display: flex;
  justify-content: space-between;
}
.btn_filter {
  border-radius: 10px;
  padding: 10px;
  background: var(--color_main);
  color: white;
}
/* end filter list */
/* drop down  */
.dropdown_input > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_input > .text,
.dropdown_input_from > .text {
  position: absolute;
  top: 50%;
  left: 50%;
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 15px !important;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  transform: translate(-50%, -50%);
  white-space: nowrap;
}
.dropdown_menu_show {
  opacity: 1 !important;
  pointer-events: visible !important;
}
.dropdown {
  width: 180px;
  margin: 10px;
  position: relative;
  margin-left: 0px;
}
.dropdown_input {
  position: relative;
  padding: 20px 20px;
  background: white;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-weight: 600;
}
.dropdown_menu {
  transition: 0.1s;
  margin-top: 3px;
  border: 1px solid var(--color_main);
  position: absolute;
  left: 0;
  right: 0;
  opacity: 0;
  z-index: 1;
  pointer-events: none;
}
.dropdown_item {
  padding: 10px;
  background: white;
}
.dropdown_item:hover {
  background: var(--color_main);
  color: white;
}
/* end drop down  */

h1 {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

/* upperbar */
.Upperbar {
  display: flex;
  min-width: 900px;
  height: 72px;
  padding: 5px 15px;
  justify-content: space-between;
  align-items: center;
  flex-shrink: 0;
  background: white;
  border-radius: 10px;
}
.Upperbar_add > button {
  padding: 10px 20px;
  color: white;
  border-radius: 10px;
  background: var(--color_main);
}
/* end upperbar */
/*
upper parent right  */
.btn_search > button {
  height: 33px !important;
  border-radius: 10px;
  background: var(--color_main);
  font-size: 12px !important;
  color: white;
}
.upper_parent {
  display: flex;
  gap: 10px;
}
.upper_parent > div {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
  gap: 5px;
}
.btn_search {
  display: flex;
  justify-content: end !important;
  border-radius: 10px;
}
.upper_parent > div > label {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 12px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
}
.field {
  width: 146px !important;
  height: 33px !important;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  flex-shrink: 0;
  position: relative;
}
.field > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_from {
  position: relative;
}
/* end uppper parent right */

/* list parent  */
.list_exam_child {
  background: white;
  border-radius: 10px;
  padding: 0 20px;
  margin-top: 19px;
  padding-bottom: 67px;
  position: relative;
  height: var(--height_from_admin);
}
/* end list parent  */
</style>
