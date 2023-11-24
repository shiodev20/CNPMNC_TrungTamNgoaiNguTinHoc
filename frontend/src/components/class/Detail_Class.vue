<template>
  <div v-if="data.id" class="list_exam">
    <h1>Chi tiết danh sách lớp học</h1>
    <div class="Upperbar">
      <div class="upperbar_add">
        <div class="upperbar_item">
          <label>Tên lớp học</label>
          <input v-model="data_id.name" disabled type="text" />
        </div>
        <div class="upperbar_item">
          <label>Ngày bắt đầu</label>
          <input v-model="data_id.start_date" disabled type="date" />
        </div>
        <div class="upperbar_item">
          <label>Ngày kết thúc</label>
          <input v-model="data_id.end_date" disabled type="date" />
        </div>
        <div class="upperbar_item">
          <label>Địa điểm</label>
          <input v-model="branch_id.name" disabled type="text" />
        </div>
      </div>
    </div>
    <div v-if="this.$route.query.add == undefined" class="list_exam_child">
      <!-- List exam -->
      <div class="list_exams">
        <div class="parent_customer">
          <div v-if="cus" class="filter_customer">
            <label class="lb_filter_cus">
              <input
                v-model="$store.state.cus_class.status_chose"
                type="radio"
                :value="1"
                name="dd"
              />
              <span
                >Thuộc lớp học <strong>{{ count_cuspay }}</strong></span
              >
            </label>
            <label class="lb_filter_cus">
              <input
                v-model="$store.state.cus_class.status_chose"
                type="radio"
                :value="2"
                name="dd"
              />
              <span
                >Chưa thanh toán <strong>{{ count_cusnotpay }}</strong></span
              >
            </label>
            <label class="lb_filter_cus">
              <input
                v-model="$store.state.cus_class.status_chose"
                type="radio"
                :value="3"
                name="dd"
              />
              <span
                >Quá hạn thanh toán
                <strong>{{ count_cusnotpayout }}</strong></span
              >
            </label>
          </div>
          <div>
            <strong>Số lượng {{ list_customer.length }}</strong>
          </div>
          <!-- <div v-if="$store.state.cus_class.status_chose == 3" class="chonse">
            <strong class="delete" @click="delete_all()">Xóa tất cả</strong>
          </div> -->
        </div>
        <table v-if="list_customer.length > 0" class="table">
          <tr>
            <th>
              Id
              <div id="clock"></div>
            </th>
            <th>Họ</th>
            <th>Tên</th>
            <th>Ngày sinh</th>
            <th v-if="$store.state.cus_class.status_chose != 1">
              Hạn thanh toán
            </th>
            <th>Chức năng</th>
          </tr>
          <tr
            :id="'item' + item.id"
            v-for="item in list_customer"
            :key="item.id"
          >
            <td>{{ cus_id(item.customer_id).id }}</td>
            <td>{{ cus_id(item.customer_id).lastname }}</td>
            <td>{{ cus_id(item.customer_id).firtname }}</td>
            <td>{{ cus_id(item.customer_id).dob }}</td>
            <td
              v-if="$store.state.cus_class.status_chose != 1"
              :id="'clock' + item.id"
            >
              {{ convert_paylimti(item) }}
              {{
                check_date(item.date_create) == true
                  ? "0 Tiếng 0 Phút 0 Giây"
                  : "Hết hạn"
              }}
            </td>
            <td class="setting">
              <button
                v-if="$store.state.cus_class.status_chose == 3"
                @click="deleteid(item.id)"
              >
                <font-awesome-icon
                  class="icon_trash"
                  :icon="['fas', 'trash-can']"
                />
              </button>
              <button
                @click="add_cus_class(item.customer_id)"
                :id="'addcus' + item.id"
                class="add_cus"
                v-if="
                  item.status_pay == false &&
                  $store.state.cus_class.status_chose != 3
                "
              >
                <span>Thêm vào lớp</span>
                <font-awesome-icon
                  class="icon"
                  :icon="['fas', 'address-book']"
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

    for (var item in this.list_customer) {
      const div = document.getElementById("item" + this.list_customer[item].id);
      if (item >= page_start && item <= page_end) {
        div.classList.contains("hide") ? div.classList.remove("hide") : null;
        div.classList.add("show");
      } else {
        div.classList.contains("show") ? div.classList.remove("show") : null;
        div.classList.add("hide");
      }
    }
  },
  created() {
    const check = setInterval(() => {
      if (store.state.class.class != null) {
        clearInterval(check);
        if (this.$route.query.action) {
          this.$route.query.action == "detail"
            ? ((store.state.class.data_post.id = Number(this.$route.query.id)),
              (store.state.class.data_post.name = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].name),
              (store.state.class.data_post.course_id = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].course_id),
              (store.state.class.data_post.branch_id = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].branch_id),
              (store.state.class.data_post.start_date = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].start_date),
              (store.state.class.data_post.end_date = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].end_date))
            : null;
        }
      }
    }, 200);
  },
  setup() {
    const toast = useToast();
    store.dispatch("class/get_all");
    store.dispatch("branch/get_all");
    store.dispatch("course/get_all");
    store.dispatch("cus_class/get_all");
    const data = computed(() => {
      return store.state.class.data_post;
    });
    const data_id = computed(() => {
      return store.getters["class/getbyid"](data.value.id)[0];
    });
    const branch_id = computed(() => {
      return store.getters["branch/getbyid"](data_id.value.branch_id)[0];
    });
    const list_customer = computed(() => {
      if (avaible.value != false) {
        array.value.forEach((e) => {
          store.dispatch("cus_class/delete", e);
        });
        // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        array.value = [];
        // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        count_cusnotpay.value =
          count_cusnotpay.value - count_cusnotpayout.value;
        // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        count_cusnotpayout.value = 0;
        return [];
      }
      if (store.state.cus_class.status_chose == 2) {
        return store.getters["cus_class/getbyidclass_payfalse"](
          store.state.class.data_post.id
        );
      }
      if (store.state.cus_class.status_chose == 3) {
        return store.getters["cus_class/getbyidclass_notpay"](
          store.state.class.data_post.id
        );
      }
      return store.getters["cus_class/getbyidclass"](
        store.state.class.data_post.id
      );
    });
    const onsubmit = () => {
      const datas = [];
      document.querySelectorAll("#data").forEach((e) => {
        if (e.checked == true) {
          datas.push(Number(e.value));
          store.dispatch("customer/add_cus");
        }
      });
    };
    const cus_id = (value) => {
      return store.getters["acount/getbyid"](value)[0];
    };
    const cus = computed(() => {
      return store.state.acount.emloyee;
    });

    const this_page = ref(1);
    const limit_page = ref(3);
    const run_app = watch(this_page, () => {
      let page_start = limit_page.value * (this_page.value - 1);
      let page_end = limit_page.value * this_page.value - 1;
      for (var item in list_customer.value) {
        const div = document.getElementById(
          "item" + list_customer.value[item].id
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
      if (list_customer.value == null) {
        return 0;
      }
      return Math.ceil(list_customer.value.length / limit_page.value);
    });

    const chage = (item) => {
      this_page.value = item;
    };

    const count_cusnotpay = computed(() => {
      return store.getters["cus_class/getbyidclass_payfalse"](
        store.state.class.data_post.id
      ).length;
    });

    const count_cusnotpayout = computed(() => {
      if (avaible.value != false) {
        return 0;
      }
      return store.getters["cus_class/getbyidclass_notpay"](
        store.state.class.data_post.id
      ).length;
    });

    const count_cuspay = computed(() => {
      return store.getters["cus_class/getbyidclass"](
        store.state.class.data_post.id
      ).length;
    });

    const convert_paylimti = (data) => {
      const value = data.date_create;
      var day = 2 * (24 * 60 * 60 * 1000);
      const result =
        day -
        Number(
          Number(new Date().getTime()) - Number(new Date(value).getTime())
        );
      const miu =
        (result / (60 * 60 * 1000) - Math.floor(result / (60 * 60 * 1000))) *
        (60 * 60 * 1000);

      const second =
        (miu / (60 * 1000) - Math.floor(miu / (60 * 1000))) * (60 * 1000);

      const time =
        Math.floor(result / (60 * 60 * 1000)) +
        " Tiếng " +
        Math.floor(miu / (60 * 1000)) +
        " phút " +
        Math.floor(second / 1000) +
        " Giây";
      if (document.getElementById("clock" + data.id) != null) {
        if (result > 0) {
          document.getElementById("clock" + data.id).innerText = time;
        } else {
          document.getElementById("clock" + data.id).innerText = "Hết Hạn";
        }
      }
      if (result > 0) {
        setTimeout(() => {
          convert_paylimti(data);
        }, 500);
      }
    };
    const check_date = (value) => {
      const date = new Date(value);
      date.setDate(Number(date.getDate()) + 2);
      if (date.getTime() <= new Date().getTime()) {
        return false;
      }
      return true;
    };
    const array = ref([]);
    const push_chose = (value) => {
      if (document.getElementById("check" + value.id).checked != true) {
        array.value.push(value.id);
      } else {
        array.value.forEach((e, index) => {
          if (e == value.id) {
            array.value.splice(index, index + 1);
          }
        });
      }
    };
    const avaible = ref(false);
    const delete_all = () => {
      // list_customer.value.forEach((e) => {
      //   array.value.push(e.id);
      // });
      // avaible.value = true;
    };
    const deleteid = (id) => {
      store.dispatch("cus_class/deleteid", id);
      toast.error("Xóa khách hàng thành công");
    };

    const add_cus_class = (value) => {
      //add cus class
      const datapost = reactive({
        id: null,
        date_create: new Date().getTime(),
        price: store.getters["course/getbyid"](
          store.getters["class/getbyid"](store.state.class.data_post.id)[0]
            .course_id
        )[0].price,
        id_class: store.state.class.data_post.id,
        id_cus: value,
      });

      const data = store.getters["cus_class/getbyid"](
        value,
        store.state.class.data_post.id
      );
      data.status_pay = true;
      console.log(data);
      store.dispatch("cus_class/update", data);
      //add bill class
      store.dispatch("bill_cus_class/add_cus", datapost);
      toast.success("Thêm vào lớp thành công");
    };
    return {
      add_cus_class,
      delete_all,
      push_chose,
      count_cusnotpay,
      data,
      data_id,
      branch_id,
      list_customer,
      onsubmit,
      cus,
      cus_id,
      count_page,
      limit_page,
      this_page,
      chage,
      run_app,
      count_cuspay,
      convert_paylimti,
      check_date,
      count_cusnotpayout,
      array,
      deleteid,
    };
  },
};
</script>
<style scoped>
.chonse {
  display: flex;
  align-items: center;
  gap: 20px;
}
.parent_customer {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.btn_delete {
  position: absolute;
  bottom: 15px;
  left: 50%;
  transform: translateX(-50%);
}
.btn_delete > button {
  background: red;
  padding: 7px 30px;
  border-radius: 5px;
  border: none;
  color: white;
}
.add_cus {
  display: flex;
  justify-content: center;
  align-items: center;
  font-weight: 700;
}
.lb_filter_cus > input {
  display: none;
}

.lb_filter_cus > span {
  padding: 10px;
  border: 2px solid var(--color_main);
  border-radius: 10px;
  position: relative;
}
.lb_filter_cus > span > strong {
  position: absolute;
  right: -10px;
  top: -10px;
  display: block;
  width: 20px;
  height: 20px;
  border-radius: 50%;
  color: white;
  background: red;
}
.lb_filter_cus > input:checked ~ span {
  color: rgb(193, 182, 182);
  background: var(--color_main);
}
.filter_customer {
  display: flex;
  justify-content: start;
  padding-bottom: 10px;
  gap: 10px;
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
.btn {
  position: relative;
  width: 25px;
  display: block;
  height: 25px;
  border-radius: 5px;
  border: 1px solid var(--color_main);
}
.group_check {
  position: relative;
}

.group_check {
  justify-content: center;
  display: flex;
}
.group_check > label {
  display: flex;
  gap: 14px;
  align-items: center;
}
label {
  margin-bottom: 9px;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 14px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}
.radio {
  display: none;
}
.radio:checked ~ .btn > .icon_check {
  display: block;
}
.icon_check {
  position: absolute;
  top: 50%;
  left: 50%;
  display: none;
  transform: translate(-50%, -50%);
}

.btnadd {
  border: none;
  border-radius: 10px;
  background: rgb(192, 2, 2);
  color: white;
  padding: 10px 40px;
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  bottom: 10px;
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
.select_table > label {
  width: 124px;
  height: 33px;
  flex-shrink: 0;
  border-radius: 10px 10px 0px 0px;
  border-bottom: none !important;
  border: 1px solid var(--color_main);
  color: rgb(67, 62, 62);
  position: relative;
}
.select_table > label > input {
  display: none;
}
.select_table > label > input:checked ~ .bg {
  background: #088395;
  color: white;
}
.bg {
  position: absolute;
  inset: 0;
  display: flex;
  justify-content: center;
  border-radius: 10px 10px 0px 0px;
  align-items: center;
  border-bottom: none !important;
  cursor: pointer;
  background: #0e6d7b;
}
.select_table {
  position: absolute;
  display: flex;
  top: -33px;
}
table {
  border-collapse: collapse;
  position: relative;
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
.setting {
  display: flex;
  justify-content: center;
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
.icon,
.icon_detail {
  font-size: 20px;
  padding: 5px;
}
.icon_trash {
  color: red;
}
.icon {
  color: green;
}
.icon_edit {
  color: rgb(85, 250, 85);
}

/* end list exam  */
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
  position: relative;
}
.upperbar_button > button {
  width: 146px;
  height: 33px;
  border-radius: 10px;
  background: var(--color_main);
  color: white;
  font-size: 13px;
}
.upperbar_button {
  position: absolute;
  right: 10px;
  bottom: 20px;
}
.upperbar_add {
  display: flex;
  justify-content: start;
  gap: 10px;
}
.upperbar_add > div {
  display: flex;
  flex-direction: column;
  align-items: start;
}
.upperbar_add > div > input {
  width: 146px;
  height: 33px;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-family: Roboto;
  font-size: 12px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}
.upperbar_add > div > label {
  margin-bottom: 5px;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 12px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
}
/* end upperbar */

/* list parent  */
.list_exam_child {
  background: white;
  border-radius: 10px;
  padding: 0 20px;
  padding-bottom: 67px;
  position: relative;
  padding-top: 50px;
  margin-top: 10px;
  height: var(--height_from_admin);
}
/* end list parent  */
</style>
