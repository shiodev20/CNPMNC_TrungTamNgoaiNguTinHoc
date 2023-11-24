<template>
  <div class="list_exam">
    <h1>Danh sách kỳ thi</h1>
    <div class="Upperbar">
      <div class="Upperbar_add">
        <button
          @click="
            this.$router.push({ name: 'exam', query: { action: 'create' } })
          "
        >
          Tạo kỳ thi
        </button>
      </div>
      <!-- <div class="Upperbar_search">
        <input type="text" />
        <button>Tìm kiếm</button>
      </div> -->
    </div>
    <div v-if="list_exam" class="list_exam_child">
      <!-- filter  -->
      <div class="parent_filter">
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
            <button class="btn_filter" @click="key_search = data_fetch.id">
              Lọc
            </button>
          </div>
        </div>
        <div>
          <strong>Số lượng 10</strong>
        </div>
      </div>
      <!-- end filter -->
      <!-- List exam -->
      <div class="list_exams">
        <table v-if="list_exam.length > 0" class="table">
          <tr>
            <th>Id</th>
            <th>Tên kỳ thi</th>
            <th>Ngôn ngữ</th>
            <th>Hình thức thi</th>
            <th>Lệ phí</th>
            <th>Chức năng</th>
          </tr>
          <tr :id="'item' + item.id" v-for="item in list_exam" :key="item.id">
            <td>{{ item.id }}</td>
            <td>{{ item.name }}</td>
            <td v-if="$store.state.languages.languages">
              {{
                $store.getters["languages/getbyid"](item.language_id)[0].name
              }}
            </td>
            <td v-if="content_exame">
              <span v-for="itemx in item.exam_type_id" :key="itemx">{{
                content_exam_id(itemx) == null
                  ? null
                  : content_exam_id(itemx).name + "  "
              }}</span>
            </td>
            <td>{{ format_money(item.price) }}</td>
            <td class="setting">
              <button
                @click="
                  this.$router.push({
                    name: 'exam',
                    query: { action: 'edit', id: item.id },
                  })
                "
              >
                <font-awesome-icon
                  class="icon_edit"
                  :icon="['fas', 'pen-fancy']"
                />
              </button>
              <button @click="deletes(item.id)">
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
  updated() {
    let page_start = this.limit_page * (this.this_page - 1);
    let page_end = this.limit_page * this.this_page - 1;

    for (var item in this.list_exam) {
      const div = document.getElementById("item" + this.list_exam[item].id);
      if (item >= page_start && item <= page_end) {
        div.classList.contains("hide") ? div.classList.remove("hide") : null;
        div.classList.add("show");
      } else {
        div.classList.contains("show") ? div.classList.remove("show") : null;
        div.classList.add("hide");
      }
    }
  },
  setup() {
    const toast = useToast();
    store.dispatch("exam/get_all");
    store.dispatch("languages/get_all");
    store.dispatch("content_exame/get_all");
    const content_exame = computed(() => {
      return store.state.content_exame.content_exame;
    });
    const format_money = (value) => {
      return value.toLocaleString("vi-VN", {
        currency: "VND",
        style: "currency",
      });
    };
    const content_exam_id = (value) => {
      return store.getters["content_exame/getbyid"](value)[0];
    };
    const data_fetch = reactive({
      name: null,
      id: null,
    });
    const status_dropdown = ref(false);
    const data = computed(() => {
      return store.state.languages.languages;
    });

    const this_page = ref(1);
    const limit_page = ref(6);

    const run_app = watch(this_page, () => {
      let page_start = limit_page.value * (this_page.value - 1);
      let page_end = limit_page.value * this_page.value - 1;
      for (var item in list_exam.value) {
        const div = document.getElementById("item" + list_exam.value[item].id);
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
      if (list_exam.value == null) {
        return 0;
      }
      return Math.ceil(list_exam.value.length / limit_page.value);
    });

    const chage = (item) => {
      this_page.value = item;
      console.log(this_page.value);
    };

    const deletes = (id) => {
      toast.error("Xóa Thành công");
      store.dispatch("exam/delete", id);
    };

    const key_search = ref(null);

    const list_exam = computed(() => {
      if (key_search.value != null) {
        return store.state.exam.exam.filter(
          (e) => e.language_id == key_search.value
        );
      }
      return store.state.exam.exam;
    });
    return {
      data,
      data_fetch,
      status_dropdown,
      list_exam,
      deletes,
      run_app,
      count_page,
      chage,
      this_page,
      limit_page,
      key_search,
      content_exame,
      content_exam_id,
      format_money,
    };
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
/* pagenigation */
.group_page {
  position: absolute;
  display: flex;
  bottom: 15px;
  gap: 10px;
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
.dropdown_input > .text {
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
  margin-left: 0;
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
  width: 912px;
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
.Upperbar_search {
  display: flex;
  gap: 10px;
}
.Upperbar_search > input {
  width: 175px;
  height: 38px;
  border-radius: 10px;
  border: 1px solid var(--color_main);
}
.Upperbar_search > button {
  width: 73px;
  height: 38px;
  color: white;
  border-radius: 10px;
  background: var(--color_main);
}
/* end upperbar */

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
